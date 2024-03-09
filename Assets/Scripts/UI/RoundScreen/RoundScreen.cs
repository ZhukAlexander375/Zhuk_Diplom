using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RoundScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _actionOfDiceFace;
    [SerializeField] private TMP_Text _displayText;
    [SerializeField] private GameObject _roundScreen;
    [SerializeField] private GameObject _resultScreen;
    [SerializeField] private GameObject _timer;
    [SerializeField] private GameObject _timeOutScreen;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private Button _buttonStartTimer;
    [SerializeField] private Button _buttonLose;
    [SerializeField] private Button _buttonWin;

    private TeamsManager teamsManager;
    private GameManager gameManager;
    private bool IsTimeOut;

    private void Awake()
    {
        teamsManager = FindObjectOfType<TeamsManager>();
        gameManager = FindObjectOfType<GameManager>();
        GameManager.OnDiceResult.AddListener(ShowAction);
        GameManager.OnWordForMove.AddListener(DisplayWord);
        Timer.OnTimeOut.AddListener(HandleTimeOut);
        Timer.OnTimeTextUpdate.AddListener(UpdateTimeScreen);
    }

    private void Start()
    {
        int currentDiceTopFace = gameManager.GetDiceTopFace();
        ShowAction(currentDiceTopFace);
    }

    private void OnEnable()
    {
        IsTimeOut = false;
        _buttonStartTimer.gameObject.SetActive(true);
        _timer.SetActive(false);
        _timerText.gameObject.SetActive(false);
        _buttonLose.gameObject.SetActive(false);
        _buttonWin.gameObject.SetActive(false);
        _timeOutScreen.SetActive(false);
    }

    private void DisplayWord(string word)
    {       
        _displayText.text = word;
    }

    public void OnButtonStartTimer()
    {
        _buttonStartTimer.gameObject.SetActive(false);
        _timer.SetActive(true);
        _timerText.gameObject.SetActive(true);
        _buttonLose.gameObject.SetActive(true);
        _buttonWin.gameObject.SetActive(true);

        gameManager.StartTimer();
    }

    private void UpdateTimeScreen(int minuts, int seconds)
    {
        _timerText.text = $"{minuts} : {seconds}";
    }
    public void ShowAction(int resultDice)
    {
        string actionText = GetActionText(resultDice);
        _actionOfDiceFace.text = actionText;
    }

    private string GetActionText(int resultDice)
    {
        switch (resultDice)
        {
            case 1: return "—ÀŒ¬¿Ã»";
            case 2: return "Õ¿Œ¡Œ–Œ“";
            case 3: return "∆≈—“€";
            case 4: return "–»—”ÕŒ ";
            case 5: return "œÀ¿—“»À»Õ*";
            case 6: return "ƒ¿/Õ≈“";
            default: return string.Empty;
        }
    }
    
    public void OnButtonLose()
    {
        //gameManager.CurrentTeamEndFailure(gameManager.GetTeamIndexForRound());
        teamsManager.CurrentTeamEndFailure();
        OpenResultScreen();
    }

    public void OnButtonWin()
    {        
        if (IsTimeOut == false)
        {
            //gameManager.CurrentTeamEndSuccess(gameManager.GetTeamIndexForRound());
            teamsManager.CurrentTeamEndSuccess();
            OpenResultScreen();
            IsTimeOut = false;
        }
        _timeOutScreen.SetActive(true);
    }

    public void OpenResultScreen()
    {
        _roundScreen.SetActive(false);
        _timer.SetActive(false);
        _resultScreen.SetActive(true);
    }

    private void HandleTimeOut()
    {
        IsTimeOut = true;
    }
    public void OnTimeOutScreen()
    {
        _timeOutScreen.SetActive(false);
    }
}
