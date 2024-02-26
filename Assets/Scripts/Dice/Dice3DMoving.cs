using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice3DMoving : MonoBehaviour
{
    [SerializeField] private float _force;
    private Rigidbody _rigidbody;
    private bool _isMoving = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {        
        if (Input.GetMouseButtonDown(0))
        {            
            _isMoving = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isMoving = false;
        }

        if (_isMoving)
        {
            MoveCube();
        }
    }

    private void MoveCube()
    {
        //var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mousePos.z = 0;
        //var direction = (transform.position - mousePos).normalized;
        //_rigidbody.AddForce(direction * _force, ForceMode.Impulse);

        Vector3 bounceForce = new Vector3(0f, 5f, 0f);
        _rigidbody.AddForce(bounceForce, ForceMode.Impulse);
    }
}
