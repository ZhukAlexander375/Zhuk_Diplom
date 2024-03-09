using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class CurrentGameInfo : MonoBehaviour
{   
    [SerializeField] private TMP_Text _membersInfoField;
    [SerializeField] private TMP_Text[] _teamsNameField;    
    [SerializeField] private TMP_Text _pointsInfo;
    [SerializeField] private TMP_Text _roundsInfo;
    [SerializeField] private GameObject _gameInfoScreen;
    [SerializeField] private GameObject _roundStartScreen;
    [SerializeField] private Button _failStartWindow;


    private GameManager gameManager;
    private TeamsManager teamsManager;
    private PointsAndRoundsManager pointsAndRoundsManager;



    private void Awake()
    {
        pointsAndRoundsManager = FindObjectOfType<PointsAndRoundsManager>();        
        teamsManager = FindObjectOfType<TeamsManager>();
        gameManager = FindObjectOfType<GameManager>();

        PointsAndRoundsManager.OnIncreaseNumOfPoints.AddListener(UpdatePointsText);
        PointsAndRoundsManager.OnDecreaseNumOfPoints.AddListener(UpdatePointsText);

        PointsAndRoundsManager.OnIncreaseNumOfRounds.AddListener(UpdateRoundsText);
        PointsAndRoundsManager.OnDecreaseNumOfRounds.AddListener(UpdateRoundsText);

        TeamsManager.UpdateTeamsSettings.AddListener(GetTeamsNames);
        TeamsManager.UpdateCountOfTeams.AddListener(UpdateNumOfMembers);

        GameManager.OnStartGame.AddListener(OnStartGame);
        GameManager.OnFailStartGame.AddListener(OnFailStartGame);
        
    }

    void Start()
    {
        _failStartWindow.gameObject.SetActive(false);
        UpdateNumOfMembers();
        UpdatePointsText();
        UpdateRoundsText();
        UpdateTeamsNames();        
    }
    private void UpdateNumOfMembers()
    {
        _membersInfoField.text = $"<color={GlobalColorSettings.textSelectionColor}>”◊¿—“¬”≈“  ŒÃ¿Õƒ:  </color> <color={GlobalColorSettings.textMainColor}>{teamsManager.GetCountOfTeams()}</color>";
    }    
    private void UpdateTeamsNames()
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

    private void UpdatePointsText()
    {
        _pointsInfo.text = $"<color={GlobalColorSettings.textSelectionColor}>Œ◊ » ƒŒ œŒ¡≈ƒ€:  </color>{pointsAndRoundsManager.GetTargetNumOfPoints()}";
    }

    private void UpdateRoundsText()
    {
        _roundsInfo.text = $"<color={GlobalColorSettings.textSelectionColor}>–¿”Õƒ€ »√–€:  </color>{pointsAndRoundsManager.GetTargetNumOfRounds()}";
    }

    public void OnButtonStartGame()
    {
        gameManager.StartGame();
    }
    private void OnStartGame()
    {
        _gameInfoScreen.SetActive(false);
        _roundStartScreen.SetActive(true);
    }   

    private void OnFailStartGame()
    {
        _failStartWindow.gameObject.SetActive(true);
    }

    public void OnButtonFailStart()
    {
        _failStartWindow.gameObject.SetActive(false);
    }
}
