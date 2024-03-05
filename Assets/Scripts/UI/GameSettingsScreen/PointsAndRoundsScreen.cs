using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class PointsAndRoundsScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _numOfPoints;
    [SerializeField] private TMP_Text _numOfRounds;
    [SerializeField] private int _stepOfPoints = 5;
    [SerializeField] private int _stepOfRounds = 1;
    [SerializeField] private Button _buttonSetTwoMinuts;
    [SerializeField] private Button _buttonUndoTwoMinuts;
    private bool IsSetTwoMinuts;


    private GameManager gameManager;
    private PointsAndRoundsManager pointsAndRoundsManager;

    private void Awake()
    {
        IsSetTwoMinuts = false;
        _buttonSetTwoMinuts.gameObject.SetActive(true);
        _buttonUndoTwoMinuts.gameObject.SetActive(false);
        pointsAndRoundsManager = FindObjectOfType<PointsAndRoundsManager>();


        PointsAndRoundsManager.OnIncreaseNumOfPoints.AddListener(UpdatePointsText);
        PointsAndRoundsManager.OnDecreaseNumOfPoints.AddListener(UpdatePointsText);
        PointsAndRoundsManager.OnIncreaseNumOfRounds.AddListener(UpdateRoundsText);
        PointsAndRoundsManager.OnDecreaseNumOfRounds.AddListener(UpdateRoundsText);
    }

    public void OnButtonPointsPlus()
    {
        pointsAndRoundsManager.IncreaseNumOfPoints(_stepOfPoints);       
    }
    public void OnButtonPointsMinus()
    {
        pointsAndRoundsManager.DecreaseNumOfPoints(_stepOfPoints);        
    }
    private void UpdatePointsText()
    {
        _numOfPoints.text = pointsAndRoundsManager.GetTargetNumOfPoints().ToString();
    }

    public void OnButtonRoundsPlus()
    {
        pointsAndRoundsManager.IncreaseNumOfRounds(_stepOfRounds);
    }
    public void OnButtonRoundsMinus()
    {
        pointsAndRoundsManager.DecreaseNumOfRounds(_stepOfRounds);
    }
    private void UpdateRoundsText()
    {
        _numOfRounds.text = pointsAndRoundsManager.GetTargetNumOfRounds().ToString();
    }   

    public void OnButtonSetTwoMinuts()
    {
        SetTwoMinuts(true);
    }

    public void OnButtonUndoTwoMinuts()
    {
        SetTwoMinuts(false);
    }

    private void SetTwoMinuts(bool value)
    {
        _buttonUndoTwoMinuts.gameObject.SetActive(value);
        IsSetTwoMinuts = value;
        pointsAndRoundsManager.IncreaseTimeForAction(IsSetTwoMinuts);
    }

}       
