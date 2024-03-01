using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class PointsAndRounds : MonoBehaviour
{
    [SerializeField] private TMP_Text _numOfPoints;
    [SerializeField] private TMP_Text _numOfRounds;
    [SerializeField] private int _stepOfPoints = 5;
    [SerializeField] private int _stepOfRounds = 1;
    [SerializeField] private Button _buttonSetTwoMinuts;
    [SerializeField] private Button _buttonUndoTwoMinuts;
    private bool IsSetTwoMinuts;


    private GameManager gameManager;

    private void Awake()
    {
        IsSetTwoMinuts = false;
        _buttonSetTwoMinuts.gameObject.SetActive(true);
        _buttonUndoTwoMinuts.gameObject.SetActive(false);
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
        gameManager.IncreaseTimeForAction(IsSetTwoMinuts);
    }

}       
