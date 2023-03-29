using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class Slerper : MonoBehaviour
{
    private Quaternion _targetAngle;
    private Quaternion _startAngle = Quaternion.Euler(0, 0, 45);
    private Quaternion _endAngle = Quaternion.Euler(0, 0, 315);
    private float _timePassed;

    private float _threshold = 3.5f;

    private void Start()
    {
        _targetAngle = _startAngle;
    }

    // Update is called once per frame
    void Update()
    {

        if (Mathf.Abs(this.transform.eulerAngles.z - _startAngle.eulerAngles.z) < _threshold ||
            Mathf.Abs(this.transform.eulerAngles.z - _endAngle.eulerAngles.z) < _threshold)
        {
            ChangeCurrentAngle();
        }
        
        _timePassed = Time.deltaTime;

        float pingpong = Mathf.PingPong(_timePassed, 1f);

     
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, _targetAngle, pingpong);
        
        
        
        
    }

    private void ChangeCurrentAngle()
    {
        if (_targetAngle.eulerAngles.z == _startAngle.eulerAngles.z)
        {
            _targetAngle = _endAngle;
        }
        else
        {
            _targetAngle = _startAngle;
        }
    }
}
