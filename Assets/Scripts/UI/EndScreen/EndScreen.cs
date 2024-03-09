using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _winnerField;
    [SerializeField] private TMP_Text _winnerPoints;

    private TeamsManager teamsManager;

    private void Awake()
    {        
        teamsManager = FindObjectOfType<TeamsManager>();
        GameManager.WinningInfo.AddListener(ShowWinners);
    }

    private void ShowWinners(int winningTeamIndex, int winningScore)
    {
        string coloredTeamNames = string.Join("\n", GetColoredTeamNames(winningTeamIndex));
        _winnerField.text = coloredTeamNames;
        _winnerPoints.text = $"ян яв╗рнл: {winningScore}";
    }

    private IEnumerable<string> GetColoredTeamNames(int winningTeamIndex)
    {
        int maxScore = teamsManager.teamScore.Values.Max();

        var maxScoreTeams = teamsManager.teamScore
            .Where(pair => pair.Value == maxScore)
            .Select(pair => new { TeamIndex = pair.Key, TeamName = teamsManager.GetTeamSettings(pair.Key) })
            .ToList();

        foreach (var maxScoreTeam in maxScoreTeams)
        {
            string coloredTeamName = $"<color={GlobalColorSettings.teamNameColor}>{maxScoreTeam.TeamName}</color>";
            yield return coloredTeamName;
        }
    }

    /*private void ShowWinner(int winningTeamIndex, int winningScore)
    {
        string winningTeamName = teamsManager.GetTeamSettings(winningTeamIndex);
        _winnerField.text = $"<color={GlobalColorSettings.teamNameColor}>{ winningTeamName}";
        _winnerPoints.text = $"ян яв╗рнл: {winningScore}";
    }*/
}

