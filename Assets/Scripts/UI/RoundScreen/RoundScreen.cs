using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RoundScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _actionOfDiceFace;
    [SerializeField] private GameObject _roundScreen;
    [SerializeField] private GameObject _resultScreen;
    [SerializeField] private GameObject _buttonStartTimer;
    [SerializeField] private GameObject _buttonLose;
    [SerializeField] private GameObject _buttonWin;
    [SerializeField] private GameObject _timer;
    [SerializeField] private GameObject _timeOutScreen;
    [SerializeField] private TMP_Text _timerText;
   
    private GameManager gameManager;
    private bool IsTimeOut;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        GameManager.OnDiceResult.AddListener(ShowAction);
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
        _buttonStartTimer.SetActive(true);
        _timer.SetActive(false);
        _timerText.gameObject.SetActive(false);
        _buttonLose.SetActive(false);
        _buttonWin.SetActive(false);
        _timeOutScreen.SetActive(false);
    }

    public void OnButtonStartTimer()
    {
        _buttonStartTimer.SetActive(false);
        _timer.SetActive(true);
        _timerText.gameObject.SetActive(true);
        _buttonLose.SetActive(true);
        _buttonWin.SetActive(true);

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
            case 1: return "словами";
            case 2: return "наоборот";
            case 3: return "жесты";
            case 4: return "рисунок";
            case 5: return "пластилин";
            case 6: return "да/нет";
            default: return string.Empty;
        }
    }
    
    public void OnButtonLose()
    {
        //gameManager.CurrentTeamEndFailure(gameManager.GetTeamIndexForRound());
        gameManager.CurrentTeamEndFailure();
        OpenResultScreen();
    }

    public void OnButtonWin()
    {        
        if (IsTimeOut == false)
        {
            //gameManager.CurrentTeamEndSuccess(gameManager.GetTeamIndexForRound());
            gameManager.CurrentTeamEndSuccess();
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
