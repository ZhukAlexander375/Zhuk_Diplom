using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PointsAndRoundsManager : MonoBehaviour
{    
    private int targetNumOfPoints = 0;
    private int targetNumOfRounds = 0;
    private int currentRound = 1;
    public static UnityEvent OnIncreaseNumOfPoints = new();
    public static UnityEvent OnDecreaseNumOfPoints = new();
    public static UnityEvent OnIncreaseNumOfRounds = new();
    public static UnityEvent OnDecreaseNumOfRounds = new();
    [SerializeField] private bool IsSetTwoMinuts = false;
       
    public void IncreaseNumOfPoints(int points)
    {
        targetNumOfPoints += points;
        OnIncreaseNumOfPoints.Invoke();
    }
    public void DecreaseNumOfPoints(int points)
    {
        targetNumOfPoints -= points;
        if (targetNumOfPoints < 0)
        {
            targetNumOfPoints = 0;
        }
        OnDecreaseNumOfPoints.Invoke();
    }
    public int GetTargetNumOfPoints()
    {
        return targetNumOfPoints;
    }

    public void IncreaseNumOfRounds(int rounds)
    {
        targetNumOfRounds += rounds;
        OnIncreaseNumOfRounds.Invoke();
    }
    public void DecreaseNumOfRounds(int rounds)
    {
        targetNumOfRounds -= rounds;
        if (targetNumOfRounds < 0)
        {
            targetNumOfRounds = 0;
        }
        OnDecreaseNumOfRounds.Invoke();
    }
    public int GetTargetNumOfRounds()
    {
        return targetNumOfRounds;
    }

    public int GetCurrentRound()
    {
        return currentRound;
    }

    public void IncreaseTimeForAction(bool isIncrease)
    {
        IsSetTwoMinuts = isIncrease;
    }

    public bool IsGivenTwoMinuts()
    {
        return IsSetTwoMinuts;
    }
}