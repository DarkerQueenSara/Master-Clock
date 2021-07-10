using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMovingPlatform : NonStaticPlatform
{
    public bool clockwise;
    public float angularSpeed = 1f;
    public float circleRad = 1f;
    
    public Transform fixedPoint;
    private Vector3 _startPosition;
    
    private float _currentAngle;

    private bool _started;
    public override void Start()
    {
        base.Start();
        if (!clockwise) angularSpeed *= -1;
        _startPosition = transform.position;
        _started = true;
    }

    void Update()
    {
        _currentAngle += angularSpeed * Time.deltaTime;
        Vector2 offset = new Vector2(Mathf.Sin(_currentAngle), Mathf.Cos(_currentAngle)) * circleRad;
        transform.position = (Vector2) fixedPoint.position + offset;
    }
    
    public void OnDisable()
    {
        transform.position = _startPosition;
        _currentAngle = 0;
    }

    public void OnEnable()
    {
        if (_started) Start();
    }
}