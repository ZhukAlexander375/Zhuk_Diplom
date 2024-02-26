using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DiceTest : MonoBehaviour, IDiceTopShow
{
    private int topFace;

    public static UnityEvent<int> OnDiceTopFace= new();
    
    public int GetNumberOfTopFace() {
        topFace = Random.Range(1, 7);               
        return topFace;
    }
}
