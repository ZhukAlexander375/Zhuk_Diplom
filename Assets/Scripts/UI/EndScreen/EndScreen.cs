using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _winnerField;

    //private GameManager gameManager;
    private TeamsManager teamsManager;

    private void Awake()
    {
        //gameManager = FindObjectOfType<GameManager>();
        teamsManager = FindObjectOfType<TeamsManager>();
        GameManager.WinningInfo.AddListener(ShowWinner);
    }

    private void ShowWinner(int winningTeamIndex, int winningScore)
    {
        string winningTeamName = teamsManager.GetTeamSettings(winningTeamIndex);
        _winnerField.text = $"победила команда: {winningTeamName} со счетом {winningScore}";
    }
}

