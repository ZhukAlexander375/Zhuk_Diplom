using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScreenSwitcher
{
    public void ShowScreen(int screenIndex);
    public void ShowNextScreen();
    public void ShowPreviousScreen();
}
