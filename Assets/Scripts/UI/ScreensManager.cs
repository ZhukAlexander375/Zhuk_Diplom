using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreensManager : MonoBehaviour, IScreenSwitcher
{    
    [SerializeField] private GameObject[] _screens;
    private int currentScreenIndex = 0;       
    
    private void Start()
    {
        _screens[0].SetActive(true);
        for (int i = 1; i < _screens.Length; i++)
        {
            _screens[i].SetActive(false);
        }
    }

    public void ShowScreen(int screenIndex)
    {
        for (int i = 0; i < _screens.Length; i++)
        {
            _screens[i].SetActive(false);
        }

        _screens[screenIndex].SetActive(true);

        currentScreenIndex = screenIndex;
    }

    public void ShowNextScreen()
    {
        int nextScreenIndex = (currentScreenIndex + 1) % _screens.Length;
        ShowScreen(nextScreenIndex);
    }

    public void ShowPreviousScreen()
    {
        int previousScreenIndex = (currentScreenIndex - 1 + _screens.Length) % _screens.Length;
        ShowScreen(previousScreenIndex);
    }    
}
