using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CreateTeamsScreen : MonoBehaviour
{
    [SerializeField] private GameObject _teamSettings;
    [SerializeField] private Button[] _buttonsAddTeam;
    [SerializeField] private Button[] _buttonsDeleteTeam;
    [SerializeField] private GameObject[] _teams;
    [SerializeField] private TeamSettingWindow _teamSettingWindow;    
    private TeamsManager teamsManager;

    private void Awake()
    {
        teamsManager = FindObjectOfType<TeamsManager>();
    }

    private void Start()
    {
        _teamSettings.SetActive(false);
        UpdateTeamButtons();
    }   
    private void UpdateTeamButtons()
    {
        int activeTeams = CountActiveTeams();

        //Debug.Log(activeTeams);

        for (int i = 0; i < _buttonsAddTeam.Length; i++)
        {
            _buttonsAddTeam[i].gameObject.SetActive(false);

            if (activeTeams < _buttonsAddTeam.Length)
            {
                _buttonsAddTeam[activeTeams].gameObject.SetActive(true);
            }
        }

        for (int j = 0; j < _buttonsDeleteTeam.Length; j ++)
        {
            _buttonsDeleteTeam[j].gameObject.SetActive(false);

            if (activeTeams > 0)
            {
                _buttonsDeleteTeam[activeTeams - 1].gameObject.SetActive(true);
            }
        }
    }

    private int CountActiveTeams()
    {
        int activeTeamsCount = 0; 
        
        for (int i = 0; i < _teams.Length; i++)
        {
            if (_teams[i].activeSelf)
            {
                activeTeamsCount++;
            }            
        }       

        return activeTeamsCount;
    }

    public void OnButtonDeleteTeam(int teamIndex)
    {
        teamsManager.DeleteTeam(teamIndex);
        _teams[teamIndex].SetActive(false);
        _buttonsAddTeam[teamIndex].gameObject.SetActive(true);

        _teamSettingWindow.CleanTeamNameFields(teamIndex);
        UpdateTeamButtons();
    }

    public void OnButtonAddTeam(int teamIndex)
    {
        _buttonsAddTeam[teamIndex].gameObject.SetActive(false);
        _teams[teamIndex].SetActive(true);
        UpdateTeamButtons();
    }
}
