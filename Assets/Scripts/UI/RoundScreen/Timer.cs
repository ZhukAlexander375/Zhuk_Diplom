using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class Timer : MonoBehaviour
{
    [SerializeField] private Slider _timerSlider;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private Image _fillImage;
    [SerializeField] private float _maxTime = 60f;
    [SerializeField] private bool _isTimerRunning;    
    private float currentTime = 0f;

    public static UnityEvent OnTimeOut = new();
    public static UnityEvent<int, int> OnTimeTextUpdate = new(); 

    void OnEnable()
    {
        _isTimerRunning = false;
        ResetTimer();       
    }    

    public void StartTimer()
    {               
        _timerSlider.maxValue = _maxTime;
        _timerSlider.value = _maxTime;
        _isTimerRunning = true;
        //Debug.Log(_maxTime.ToString());
    }

    void Update()
    {
        if (!_isTimerRunning) return;  
        
        _timerSlider.value -= Time.deltaTime;
        _fillImage.color = _gradient.Evaluate(_timerSlider.value / _timerSlider.maxValue);

        UpdateTimerText();

        if (_timerSlider.value <= 0)
        {            
            OnTimeOut.Invoke();
        }
    }

    private void UpdateTimerText()
    {
        float timeText = _timerSlider.value;
        int minuts = Mathf.FloorToInt(timeText / 60);
        int seconds = Mathf.FloorToInt(timeText % 60);
        OnTimeTextUpdate.Invoke(minuts, seconds);
    }

    public void ResetTimer()
    {        
        currentTime = 0f;
        _timerSlider.value = currentTime;
    }
    private void StopTimer()
    {
        _maxTime = 60f;
        _isTimerRunning = false;        
    }

    private void OnDisable()
    {
        StopTimer();        
    }

    public void SetTwoMinuts(float maxTime)
    {
        _maxTime = maxTime;
    }

    public void SetOneMinut(float maxTime)
    {
        _maxTime = maxTime;
    }
}
