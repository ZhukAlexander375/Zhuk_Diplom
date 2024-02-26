using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class PointsAndRounds : MonoBehaviour
{
    [SerializeField] private TMP_Text _numOfPoints;
    [SerializeField] private TMP_Text _numOfRounds;
    [SerializeField] private int _stepOfPoints = 5;
    [SerializeField] private int _stepOfRounds = 1;    

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        GameManager.OnIncreaseNumOfPoints.AddListener(UpdatePointsText);
        GameManager.OnDecreaseNumOfPoints.AddListener(UpdatePointsText);
        GameManager.OnIncreaseNumOfRounds.AddListener(UpdateRoundsText);
        GameManager.OnDecreaseNumOfRounds.AddListener(UpdateRoundsText);
    }

    public void OnButtonPointsPlus()
    {
        gameManager.IncreaseNumOfPoints(_stepOfPoints);       
    }
    public void OnButtonPointsMinus()
    {
        gameManager.DecreaseNumOfPoints(_stepOfPoints);        
    }
    private void UpdatePointsText()
    {
        _numOfPoints.text = gameManager.GetTargetNumOfPoints().ToString();
    }

    public void OnButtonRoundsPlus()
    {
        gameManager.IncreaseNumOfRounds(_stepOfRounds);
    }
    public void OnButtonRoundsMinus()
    {
        gameManager.DecreaseNumOfRounds(_stepOfRounds);
    }
    private void UpdateRoundsText()
    {
        _numOfRounds.text = gameManager.GetTargetNumOfRounds().ToString();
    }   
}       
