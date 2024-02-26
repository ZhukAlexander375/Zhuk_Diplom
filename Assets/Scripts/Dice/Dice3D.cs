using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dice3D : MonoBehaviour, IDiceTopShow
{
    [SerializeField] private Transform[] _diceFaces;

    [SerializeField] private bool _delayFinished;
    [SerializeField] private bool _isDiceStopped;
    
    [SerializeField] private float _speed;
     
    private Rigidbody _rigidbody;

    public UnityEvent <int> OnGetDiceResult = new();
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
    }    

    private void FixedUpdate()
    {
        GetNumberOfTopFace();
        _speed = _rigidbody.velocity.magnitude;

        if (!_delayFinished) return;

        if (!_isDiceStopped && Mathf.Approximately(_speed, 0f))
        {
            _isDiceStopped = true;
            GetNumberOfTopFace();
        }
    }

    [ContextMenu("Test To Get Top")]
    public int GetNumberOfTopFace()
    {
        if (_diceFaces == null) return -1;

        float topYPosition = 0f;
        var topFace = 0;
        
        for (int i = 0; i < _diceFaces.Length; i++)
        {
            if (_diceFaces[i].position.y > topYPosition) {
                topYPosition = _diceFaces[i].position.y;
                topFace = i;  
            }
            
        }
        OnGetDiceResult.Invoke(topFace + 1);
        Debug.Log($"Dice top {topFace + 1}");
        //Debug.Log("Dice stopped");
        return topFace + 1;        
    }   
}
