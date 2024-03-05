using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PointsAndRoundsManager pointsAndRoundsManager;
    private TeamsManager teamsManager;
    private WordDatabaseReader wordDatabaseReader;
    
    public static UnityEvent OnStartGame = new();
    public static UnityEvent OnFailStartGame = new();    

    [Header("Start Game")]
    private int currentRound = 1;    
    private int currentDiceTopFace;
    public static UnityEvent UpdateCurrentTeamCounter = new();
    public static UnityEvent OnRoundStart = new();

    [Header("Round")]
    private IDiceTopShow _dice;
    [SerializeField] private Timer _timer;    
    public static UnityEvent<int, int> UpdateRoundInfo = new();
    public static UnityEvent<string> UpdateCurrentTeam = new();
    public static UnityEvent<int> OnDiceResult = new();
    public static UnityEvent<int, int> UpdateScore = new();
    public static UnityEvent<string> OnWordForMove = new();
    private string currentWordForMove;
    private int moveCounter = 1;
    
    [Header("EndGame")]
    public static UnityEvent OnEndGame = new();
    public static UnityEvent <int, int> WinningInfo= new();
    

    private void Awake()
    {
        pointsAndRoundsManager = FindObjectOfType<PointsAndRoundsManager>();
        teamsManager = FindObjectOfType<TeamsManager>();
        wordDatabaseReader = FindObjectOfType<WordDatabaseReader>();
        _dice = FindObjectOfType<DiceTest>();
    }
    public void StartGame()
    {
        var numOfteams = teamsManager.GetCountOfTeams();
        if (numOfteams >= 2 && pointsAndRoundsManager.GetTargetNumOfPoints() > 0 && pointsAndRoundsManager.GetTargetNumOfRounds() > 0)
        {
            OnStartGame.Invoke();
            StartMove();
        }
        OnFailStartGame.Invoke();
    }
    public void StartMove()    
    {   
        string currentTeam = teamsManager.GetTeamForRound();       
        //Debug.Log($"Ход этой команды {curTeamMove}");
        UpdateRoundInfo.Invoke(currentRound, pointsAndRoundsManager.GetTargetNumOfRounds());
        UpdateCurrentTeam.Invoke(currentTeam);        
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
        if (pointsAndRoundsManager.IsGivenTwoMinuts() && currentDiceTopFace == 6)
        {
            _timer.SetTwoMinuts(120f);
            _timer.StartTimer();
            return;
        }
        
        _timer.StartTimer();
    }    
    public void EndMove()
    {
        if (moveCounter < teamsManager.GetCountOfTeams())
        { 
            UpdateCurrentTeamCounter.Invoke();
            moveCounter++;
            StartMove();
        }
        else if (moveCounter >= teamsManager.GetCountOfTeams())
        {
            CheckWinningTeam();
        }

        if (currentRound > pointsAndRoundsManager.GetTargetNumOfRounds())
        {
            CheckMaxScoreTeam();
        }
    }
    private void CheckWinningTeam()
    {
        var winningTeam = teamsManager.teamScore.FirstOrDefault(x => x.Value >= pointsAndRoundsManager.GetTargetNumOfPoints());
        if (!winningTeam.Equals(default(KeyValuePair<int, int>)))
        {
            int winningTeamIndex = winningTeam.Key;
            int winningTeamScore = winningTeam.Value;            
           
            WinningInfo.Invoke(winningTeamIndex, winningTeamScore);
            EndGame();
        }
        else
        {
            UpdateCurrentTeamCounter.Invoke();
            moveCounter = 1;
            currentRound++;
            StartMove();
        }
    }
    private void CheckMaxScoreTeam()
    {
        int maxScore = teamsManager.teamScore.Values.Max();
        var maxScoreTeam = teamsManager.teamScore.FirstOrDefault(x => x.Value == maxScore);

        if (!maxScoreTeam.Equals(default(KeyValuePair<int, int>)))
        {
            int maxScoreTeamIndex = maxScoreTeam.Key;
            int maxScoreTeamScore = maxScoreTeam.Value;          
            
            WinningInfo.Invoke(maxScoreTeamIndex, maxScoreTeamScore); 
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
