using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultsScreen : MonoBehaviour
{
    [SerializeField] private GameObject _resultScreen;
    [SerializeField] private GameObject _roundStartScreen;
    [SerializeField] private GameObject _endScreen;    
    [SerializeField] private TMP_Text[] _teamsNameField;
    [SerializeField] private TMP_Text[] _teamsResultField;
        
    private PointsAndRoundsManager pointsAndRoundsManager;
    private TeamsManager teamsManager;
    private GameManager gameManager;

    private void Awake()
    {
        pointsAndRoundsManager = FindObjectOfType<PointsAndRoundsManager>();
        teamsManager = FindObjectOfType<TeamsManager>();
        gameManager = FindObjectOfType<GameManager>();
        TeamsManager.UpdateScore.AddListener(UpdateResulScreens);
        GameManager.OnEndGame.AddListener(OpenEndScreen);
    }

    private void Start()
    {
        for (int i = 0; i < teamsManager.GetCountOfTeams(); i++)
        {           
            GetTeamsNames(i);
        }           
    }    
    
    private void GetTeamsNames(int indexTeam)
    {
        if (indexTeam >= 0 && indexTeam < _teamsNameField.Length)
        {
            if (!string.IsNullOrWhiteSpace(teamsManager.GetTeamSettings(indexTeam)))
            {
                _teamsNameField[indexTeam].text = $"{indexTeam + 1}: {teamsManager.GetTeamSettings(indexTeam)}";               
            }
            else
            {
                _teamsNameField[indexTeam].text = "";            
            }
        }
    }

    private void UpdateResulScreens(int indexTeam, int points)
    {
        if (indexTeam >= 0 && indexTeam < _teamsResultField.Length)
        {
            if (!string.IsNullOrWhiteSpace(teamsManager.GetTeamSettings(indexTeam)))
            {
                _teamsResultField[indexTeam].text = $"{points} / {pointsAndRoundsManager.GetTargetNumOfPoints()}";
            }
            else
            {
                _teamsResultField[indexTeam].text = "";
            }
        }
    }

    public void OpenEndScreen()
    {
        _resultScreen.SetActive(false);
        _endScreen.SetActive(true);
    }

    public void OnButtonContinue()
    {
        gameManager.EndMove();
        _resultScreen.SetActive(false);
        _roundStartScreen.SetActive(true);
    }
}
