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
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        GameManager.UpdateScore.AddListener(UpdateResulScreens);
        GameManager.OnEndGame.AddListener(OpenEndScreen);
    }

    private void Start()
    {
        for (int i = 0; i < gameManager.GetCountOfTeams(); i++)
        {           
            GetTeamsNames(i);
        }           
    }    
    
    private void GetTeamsNames(int indexTeam)
    {
        if (indexTeam >= 0 && indexTeam < _teamsNameField.Length)
        {
            if (!string.IsNullOrWhiteSpace(gameManager.GetTeamSettings(indexTeam)))
            {
                _teamsNameField[indexTeam].text = $"{indexTeam + 1}: {gameManager.GetTeamSettings(indexTeam)}";               
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
            if (!string.IsNullOrWhiteSpace(gameManager.GetTeamSettings(indexTeam)))
            {
                _teamsResultField[indexTeam].text = $"{points}/{gameManager.GetTargetNumOfPoints()}";
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
