using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundStartScreen : MonoBehaviour
{
    private int _currentNumOfRound;

    [SerializeField] private TMP_Text _currentRoundField;
    [SerializeField] private TMP_Text _currentTeamField;
    [SerializeField] private GameObject _diceResult;
    [SerializeField] private TMP_Text _diceResultField;
    [SerializeField] private TMP_Text _actionOfDiceFace;
    [SerializeField] private GameObject _buttonRollDice;
    [SerializeField] private GameObject _buttonStartRound;
    [SerializeField] private GameObject _roundStartScreen;
    [SerializeField] private GameObject _roundScreen;
    

    private GameManager gameManager;
    private int startRound;
    private int targetRound;
    private string currentTeamForRound;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();       

        GameManager.UpdateRoundInfo.AddListener(UpdateCurrentRoundInfo);
        GameManager.UpdateCurrentTeam.AddListener(UpdateCurrentTeamInfo);
        GameManager.OnDiceResult.AddListener(ShowRollDiceResult);
        GameManager.OnDiceResult.AddListener(ShowAction);
    }

    private void Start()
    {
        startRound = gameManager.GetCurrentRound();
        targetRound = gameManager.GetTargetNumOfRounds();
        //currentTeamForRound = gameManager.GetTeamForRound();

        _buttonRollDice.SetActive(true);
        _buttonStartRound.SetActive(false);
        _diceResult.SetActive(false);

        UpdateCurrentRoundInfo(startRound, targetRound);
        UpdateCurrentTeamInfo(currentTeamForRound);
    }   

    private void UpdateCurrentRoundInfo(int currentRound, int targetRound)
    {
        _currentRoundField.text = $"����� {currentRound} �� {targetRound}";
    }

    private void UpdateCurrentTeamInfo(string teamName)
    {
        if (!_buttonStartRound.activeSelf)
        {
            _currentTeamField.text = $"��� �������: {gameManager.GetTeamForRound()}";
        }
    }

    public void OnButtonRollDice()
    {
        _diceResult.SetActive(true);
        _buttonRollDice.SetActive(false);
        _buttonStartRound.SetActive(true);

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
            case 1: return "�������";
            case 2: return "��������";
            case 3: return "�����";
            case 4: return "�������";
            case 5: return "���������";
            case 6: return "��/���";
            default: return string.Empty;
        }
    }
    public void OnButtonStartRound()
    {
        _roundScreen.SetActive(true);
        _roundStartScreen.SetActive(false);       
    }

    private void OnDisable()
    {
        _diceResult.SetActive(false);
        _buttonRollDice.SetActive(true);
        _buttonStartRound.SetActive(false);
    }
}
