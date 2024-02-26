using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TeamSettingWindow : MonoBehaviour
{
    [SerializeField] private GameObject _teamSettings;    
    [SerializeField] private List<TMP_Text> _teamNameFields;    
    [SerializeField] private TMP_InputField _inputField;    

    private GameManager gameManager;
    private int currentTeamIndex;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    
    public void OnButtonTeamNameChange(int teamIndex)
    {        
        currentTeamIndex = teamIndex;
        //Debug.Log($"��� ������ � �������� {currentTeamIndex}");
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
            gameManager.SetTeamSettings(currentTeamIndex, text);
            //Debug.Log($"����� SetTeamSettings ��������� ����� {text} � � ��������  {currentTeamIndex}");
            if (currentTeamIndex >= 0 && currentTeamIndex < _teamNameFields.Count)
            {
                _teamNameFields[currentTeamIndex].text = gameManager.GetTeamSettings(currentTeamIndex);
                //Debug.Log($"������ ������� � ��������� {_teamNameFields[currentTeamIndex].text} � �������� {currentTeamIndex} ");
            }
        }        
        CloseSettings();
    }
    
    public void CleanTeamNameFields(int index)
    {
        _teamNameFields[index].text = "";
    }
}
