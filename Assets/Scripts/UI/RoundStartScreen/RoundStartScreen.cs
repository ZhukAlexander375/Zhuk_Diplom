using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RoundStartScreen : MonoBehaviour
{
    private int _currentNumOfRound;

    [SerializeField] private TMP_Text _currentRoundField;
    [SerializeField] private TMP_Text _currentTeamField;
    [SerializeField] private GameObject _diceResult;
    [SerializeField] private TMP_Text _diceResultField;
    [SerializeField] private TMP_Text _actionOfDiceFace;
    [SerializeField] private Button _buttonRollDice;
    [SerializeField] private Button _buttonStartRound;
    [SerializeField] private GameObject _roundStartScreen;
    [SerializeField] private GameObject _roundScreen;

    private GameManager gameManager;
    private PointsAndRoundsManager pointsAndRoundsManager;
    private TeamsManager teamsManager;

    private int startRound;
    private int targetRound;
    private string currentTeamForRound;

    private void Awake()
    {
        pointsAndRoundsManager = FindObjectOfType<PointsAndRoundsManager>();
        gameManager = FindObjectOfType<GameManager>();
        teamsManager = FindObjectOfType<TeamsManager>();



        GameManager.UpdateRoundInfo.AddListener(UpdateCurrentRoundInfo);
        GameManager.UpdateCurrentTeam.AddListener(UpdateCurrentTeamInfo);
        GameManager.OnDiceResult.AddListener(ShowRollDiceResult);
        GameManager.OnDiceResult.AddListener(ShowAction);
    }

    private void Start()
    {
        startRound = pointsAndRoundsManager.GetCurrentRound();
        targetRound = pointsAndRoundsManager.GetTargetNumOfRounds();

        //currentTeamForRound = gameManager.GetTeamForRound();

        _buttonRollDice.gameObject.SetActive(true);
        _buttonStartRound.gameObject.SetActive(false);
        _diceResult.SetActive(false);

        UpdateCurrentRoundInfo(startRound, targetRound);
        UpdateCurrentTeamInfo(currentTeamForRound);
    }   

    private void UpdateCurrentRoundInfo(int currentRound, int targetRound)
    {
        _currentRoundField.text = $"Раунд {currentRound} из {targetRound}";
    }

    private void UpdateCurrentTeamInfo(string teamName)
    {
        if (!_buttonStartRound.gameObject.activeSelf)
        {
            _currentTeamField.text = $"ход команды: {teamsManager.GetTeamForRound()}";
        }
    }

    public void OnButtonRollDice()
    {
        _diceResult.SetActive(true);
        _buttonRollDice.gameObject.SetActive(false);
        _buttonStartRound.gameObject.SetActive(true);

        gameManager.RollDice();
    }

    private void ShowRollDiceResult(int diceTopFace)
    {
        _diceResultField.text = diceTopFace.ToString();
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
            case 5: return "пластилин*";
            case 6: return "да/нет";
            default: return string.Empty;
        }
    }
    public void OnButtonStartRound()
    {
        _roundScreen.SetActive(true);
        _roundStartScreen.SetActive(false);        
        gameManager.DisplayRandomWord();
    }

    private void OnDisable()
    {
        _diceResult.SetActive(false);
        _buttonRollDice.gameObject.SetActive(true);
        _buttonStartRound.gameObject.SetActive(false);
    }
}
