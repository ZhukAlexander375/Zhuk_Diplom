using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreateTemsScreen : MonoBehaviour
{
    [SerializeField] private GameObject _teamSettings;
    [SerializeField] private GameObject[] _buttonsAddTeam;
    [SerializeField] private GameObject[] _teams;

    [SerializeField] private TeamSettingWindow _teamSettingWindow;
    private GameManager gameManager;

    private void Awake()
    {        
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        _teamSettings.SetActive(false);
        UpdateTeamButtons();
    }   
    private void UpdateTeamButtons()
    {
        for (int i = 0; i < _teams.Length; i++)
        {
            if (_teams[i].activeSelf)
            {
                _buttonsAddTeam[i].SetActive(false);
            }
            else
            {
                _buttonsAddTeam[i].SetActive(true);
            }
        }
    }
   
    public void OnButtonDeleteTeam(int teamIndex)
    {
        gameManager.DeleteTeam(teamIndex);
        _teams[teamIndex].SetActive(false);
        _buttonsAddTeam[teamIndex].SetActive(true);

        _teamSettingWindow.CleanTeamNameFields(teamIndex);
    }

    public void OnButtonAddTeam(int teamIndex)
    {
        _buttonsAddTeam[teamIndex].SetActive(false);
        _teams[teamIndex].SetActive(true);
    }
}
