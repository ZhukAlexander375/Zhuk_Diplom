using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("For Points And Rounds")]
    private int targetNumOfPoints = 0;
    private int targetNumOfRounds = 0;
    public static UnityEvent OnIncreaseNumOfPoints = new();
    public static UnityEvent OnDecreaseNumOfPoints = new();
    public static UnityEvent OnIncreaseNumOfRounds = new();
    public static UnityEvent OnDecreaseNumOfRounds = new();
    [SerializeField] private bool IsSetTwoMinuts = false;


    [Header("For Create Teams")]
    private Dictionary<int, string> teamNames = new();
    private Dictionary<int, int> teamScore = new();
    public static UnityEvent<int> UpdateTeamsSettings = new();
    public static UnityEvent UpdateCountOfTeams = new();
    public static UnityEvent OnStartGame = new();
    public static UnityEvent OnFailStartGame = new();
    //private string enteredText;
    //public string EnteredText
    //{
    //    get { return enteredText; }
    //}


    [Header("Start Game")]
    private int currentRound = 1;
    private int currentPoints = 0;
    private int currentDiceTopFace = 0;
    private int currentTeamCounter = 0;
    public static UnityEvent OnRoundStart = new();

    [Header("Round")]
    [SerializeField] private Timer _timer;    
    public static UnityEvent<int, int> UpdateRoundInfo = new();
    public static UnityEvent<string> UpdateCurrentTeam = new();
    public static UnityEvent<int> OnDiceResult = new();
    public static UnityEvent<int, int> UpdateScore = new();
    public static UnityEvent<string> OnWordForMove = new();
    private WordDatabaseReader wordDatabaseReader;
    string currentWordForMove;
    private IDiceTopShow _dice;
    private int moveCounter = 1;
    

    [Header("EndGame")]
    public static UnityEvent OnEndGame = new();
    public static UnityEvent <int, int> FastWinningInfo= new();
    
    private void Awake()
    {
        wordDatabaseReader = FindObjectOfType<WordDatabaseReader>();
        _dice = FindObjectOfType<DiceTest>();
    }

    #region PointAndRounds
    public void IncreaseNumOfPoints(int points)
    {
        targetNumOfPoints += points;
        OnIncreaseNumOfPoints.Invoke();
    }
    public void DecreaseNumOfPoints(int points)
    {
        targetNumOfPoints -= points;
        if (targetNumOfPoints < 0)
        {
            targetNumOfPoints = 0;
        }
        OnDecreaseNumOfPoints.Invoke();
    }
    public int GetTargetNumOfPoints()
    {
        return targetNumOfPoints;
    }

    public void IncreaseNumOfRounds(int rounds)
    {
        targetNumOfRounds += rounds;
        OnIncreaseNumOfRounds.Invoke();
    }
    public void DecreaseNumOfRounds(int rounds)
    {
        targetNumOfRounds -= rounds;
        if (targetNumOfRounds < 0)
        {
            targetNumOfRounds = 0;
        }
        OnDecreaseNumOfRounds.Invoke();
    }
    public int GetTargetNumOfRounds()
    {
        return targetNumOfRounds;
    }

    public int GetCurrentRound()
    {
        return currentRound;
    }

    public void IncreaseTimeForAction(bool isIncrease)
    {
        IsSetTwoMinuts = isIncrease;
    }
    #endregion

    #region CreateTeams and TeamSettings   

    public void SetTeamSettings(int teamIndex, string teamName)
    {
        //Debug.Log($"создал команду {teamNames[teamIndex]} с названием {teamName}");
        teamNames[teamIndex] = teamName;
        teamScore[teamIndex] = currentPoints;
        //Debug.Log($"создал команду с индексом{teamIndex} с названием {teamName}");
        //Debug.Log($"чекну teamNames" + string.Join(", ", teamNames));
        //Debug.Log($"чекну teamScore" + string.Join(", ", teamScore));
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
        //Debug.Log($"удалил команду с индексом {teamIndex} и названием");
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
    #endregion

    #region GameLoop
    public void StartGame()
    {
        var numOfteams = teamNames.Count;
        if (numOfteams >= 2 && targetNumOfPoints > 0 && targetNumOfRounds > 0)
        {
            OnStartGame.Invoke();
            StartMove();
        }
        OnFailStartGame.Invoke();

    }
    public void StartMove()    
    {   
        string currentTeam = GetTeamForRound();       
        //Debug.Log($"Ход этой команды {curTeamMove}");
        UpdateRoundInfo.Invoke(currentRound, targetNumOfRounds);
        UpdateCurrentTeam.Invoke(currentTeam);        
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
    public int RollDice()
    {
        int diceTopFace = _dice.GetNumberOfTopFace();
        OnDiceResult.Invoke(diceTopFace);
        currentDiceTopFace = diceTopFace;
        return diceTopFace;
    }

    public int GetDiceTopFace()
    {
        return currentDiceTopFace;
    }

    public void DisplayRandomWord()
    {        
        currentWordForMove = wordDatabaseReader.GetRandomWord();
        OnWordForMove.Invoke(currentWordForMove);       
    }

    public void StartTimer()
    {
        //Debug.Log(IsSetTwoMinuts.ToString());
        //Debug.Log(currentDiceTopFace);
        if (IsSetTwoMinuts && currentDiceTopFace == 6)
        {
            _timer.SetTwoMinuts(120f);
            _timer.StartTimer();
            return;
        }
        
        _timer.StartTimer();
    }

    public void CurrentTeamEndSuccess()
    {
        int victoryPoints = GetDiceTopFace();
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

    public void EndMove()
    {
        if (moveCounter < GetCountOfTeams())
        { 
            currentTeamCounter++;
            moveCounter++;
            StartMove();
        }
        else if (moveCounter >= GetCountOfTeams())
        {
            CheckWinningTeam();
        }

        if (currentRound > targetNumOfRounds)
        {
            CheckMaxScoreTeam();
        }
    }
    private void CheckWinningTeam()
    {
        var winningTeam = teamScore.FirstOrDefault(x => x.Value >= targetNumOfPoints);
        if (!winningTeam.Equals(default(KeyValuePair<int, int>)))
        {
            int winningTeamIndex = winningTeam.Key;
            int winningTeamScore = winningTeam.Value;            
           
            FastWinningInfo.Invoke(winningTeamIndex, winningTeamScore);
            EndGame();
        }
        else
        {
            currentTeamCounter++;
            moveCounter = 1;
            currentRound++;
            StartMove();
        }
    }

    private void CheckMaxScoreTeam()
    {
        int maxScore = teamScore.Values.Max();
        var maxScoreTeam = teamScore.FirstOrDefault(x => x.Value == maxScore);

        if (!maxScoreTeam.Equals(default(KeyValuePair<int, int>)))
        {
            int maxScoreTeamIndex = maxScoreTeam.Key;
            int maxScoreTeamScore = maxScoreTeam.Value;          
            
            FastWinningInfo.Invoke(maxScoreTeamIndex, maxScoreTeamScore); 
            EndGame();
        }
    }
    
    private void EndGame()
    {
        OnEndGame.Invoke();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}   
#endregion
