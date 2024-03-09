using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalColorSettings : MonoBehaviour
{
    public static string textMainColor; //black
    public static string textSelectionColor; //grey
    public static string teamNameColor; //orange

    private void Awake()
    {
        textMainColor = "#0A0A09";
        textSelectionColor = "#69625D";
        teamNameColor = "#FF561C";

    }
}
