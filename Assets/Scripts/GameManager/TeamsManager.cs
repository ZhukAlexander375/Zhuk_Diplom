using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class TeamsManager : MonoBehaviour
{    
    public Dictionary<int, string> teamNames = new();
    public Dictionary<int, int> teamScore = new();
    public static UnityEvent<int> UpdateTeamsSettings = new();
    public static UnityEvent UpdateCountOfTeams = new();
    public static UnityEvent<int, int> UpdateScore = new();    
    private int currentPoints = 0;
    private int currentTeamCounter = 0;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        GameManager.UpdateCurrentTeamCounter.AddListener(IncreaseCurrentTeamCounter);
    }
    public void SetTeamSettings(int teamIndex, string teamName)
    {        
        teamNames[teamIndex] = teamName;
        teamScore[teamIndex] = currentPoints;
        //Debug.Log($"создал команду с индексом {teamIndex} с названием {teamName}");
        //Debug.Log($"чекну teamNames " + string.Join(", ", teamNames));
        //Debug.Log($"чекну teamScore " + string.Join(", ", teamScore));
        UpdateTeamInfoEvent(teamIndex);
    }
    public string GetTeamSettings(int teamIndex)
    {
        if (teamNames.ContainsKey(teamIndex))
        {
            return teamNames[teamIndex];
        }
        return "";
    }

    public void DeleteTeam(int teamIndex)
    {
        if (teamNames.ContainsKey(teamIndex))
        {
            teamNames.Remove(teamIndex);
            teamScore.Remove(teamIndex);
        }
        //Debug.Log($"удалил команду с индексом {teamIndex}");
        //Debug.Log($"после удаления teamNames" + string.Join(", ", teamNames));
        //Debug.Log($"чекну после удаления teamScore" + string.Join(", ", teamScore));
        UpdateTeamInfoEvent(teamIndex);
    }

    public void UpdateTeamInfoEvent(int teamIndex)
    {
        UpdateTeamsSettings.Invoke(teamIndex);
        UpdateCountOfTeams.Invoke();
    }

    public int GetCountOfTeams()
    {
        int numOfteams = teamNames.Count;
        return numOfteams;
    }

    public string GetTeamForRound()
    {
        int teamIndex = (currentTeamCounter % GetCountOfTeams());
        //Debug.Log($"индекс команды в раунде {teamIndex}");  //sm 2 raza vizov?      
        return GetTeamSettings(teamIndex);
    }

    public int GetTeamIndexForRound()
    {
        int teamIndex = (currentTeamCounter % GetCountOfTeams());
        return teamIndex;
    }

    public void CurrentTeamEndSuccess()
    {
        int victoryPoints = gameManager.GetDiceTopFace();
        CurrentTeamEndMove(victoryPoints);
    }

    public void CurrentTeamEndFailure()
    {
        int failPoints = 0;
        CurrentTeamEndMove(failPoints);
    }

    public void CurrentTeamEndMove(int points)
    {
        int teamIndex = GetTeamIndexForRound();
        UpdateCurrentTeamScore(teamIndex, points);

        //Debug.Log("посмотрим очечи" + string.Join(", ", teamScore));
        UpdateScore.Invoke(teamIndex, teamScore[teamIndex]);
    }

    private void UpdateCurrentTeamScore(int teamIndex, int points)
    {
        if (teamScore.ContainsKey(teamIndex))
        {
            teamScore[teamIndex] += points;
        }
    }

    public void IncreaseCurrentTeamCounter()
    {
        currentTeamCounter++;
    }
}
