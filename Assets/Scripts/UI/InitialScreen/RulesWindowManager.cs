using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RulesWindowManager : MonoBehaviour, IScreenSwitcher
{
    [SerializeField] private GameObject[] _rulesWindows;
    private int currentRulesWindowIndex = 0;

    [SerializeField] private GameObject _rulesShortWindow;
    [SerializeField] private Button _buttonCallRulesShort;   
    
    private void Start()   
    {
        _rulesShortWindow.SetActive(false);
        for (int i = 1; i < _rulesWindows.Length; i++)
        {
            _rulesWindows[i].SetActive(false);
        }
    }

    public void ShowScreen(int ruleWindowIndex)
    {
        for (int i = 0; i < _rulesWindows.Length; i++)
        {
            _rulesWindows[i].SetActive(false);
        }

        _rulesWindows[ruleWindowIndex].SetActive(true);

        currentRulesWindowIndex = ruleWindowIndex;
    }

    public void ShowNextScreen()
    {
        int nextScreenIndex = (currentRulesWindowIndex + 1) % _rulesWindows.Length;
        ShowScreen(nextScreenIndex);
    }

    public void ShowPreviousScreen()
    {
        int previousScreenIndex = (currentRulesWindowIndex - 1 + _rulesWindows.Length) % _rulesWindows.Length;
        ShowScreen(previousScreenIndex);

    }
    public void ShowRulesShort()
    {
        _rulesShortWindow.SetActive(true);
        _buttonCallRulesShort.gameObject.SetActive(false);
    }

    public void CloseRulesShort()
    {
        _rulesShortWindow.SetActive(false);
        _buttonCallRulesShort.gameObject.SetActive(true);
    }
}