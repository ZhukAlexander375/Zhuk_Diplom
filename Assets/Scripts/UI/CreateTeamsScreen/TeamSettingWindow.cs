using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TeamSettingWindow : MonoBehaviour
{
    [SerializeField] private GameObject _teamSettings;    
    [SerializeField] private List<TMP_Text> _teamNameFields;    
    [SerializeField] private TMP_InputField _inputField;    
        
    private TeamsManager teamsManager;
    private int currentTeamIndex;

    private void Awake()
    {
        teamsManager = FindObjectOfType<TeamsManager>();        
    }
    
    public void OnButtonSetTeamName(int teamIndex)
    {        
        currentTeamIndex = teamIndex;
        //Debug.Log($"жму кнопку с индексом {currentTeamIndex}");
        _teamSettings.SetActive(true);
    }

    public void CloseSettings()
    {
        _teamSettings.SetActive(false);
    }

    public void OnButtonApplySettings()
    {        
        string text = _inputField.text;
        if (!string.IsNullOrWhiteSpace(text))
        {
            teamsManager.SetTeamSettings(currentTeamIndex, text);
            //Debug.Log($"вызов SetTeamSettings введенный текст {text} с и индексом  {currentTeamIndex}");
            if (currentTeamIndex >= 0 && currentTeamIndex < _teamNameFields.Count)
            {
                _teamNameFields[currentTeamIndex].text = teamsManager.GetTeamSettings(currentTeamIndex);
                //Debug.Log($"создаю команду с названием {_teamNameFields[currentTeamIndex].text} и индексом {currentTeamIndex} ");
            }
        }
        _inputField.text = "";
        CloseSettings();
    }
    
    public void CleanTeamNameFields(int index)
    {
        _teamNameFields[index].text = "";
    }
}
