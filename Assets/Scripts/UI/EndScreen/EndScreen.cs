using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _winnerField;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        GameManager.FastWinningInfo.AddListener(ShowWinner);
    }

    private void ShowWinner(int winningTeamIndex, int winningScore)
    {
        string winningTeamName = gameManager.GetTeamSettings(winningTeamIndex);
        _winnerField.text = $"победила команда: {winningTeamName} со счетом {winningScore}";
    }
}

