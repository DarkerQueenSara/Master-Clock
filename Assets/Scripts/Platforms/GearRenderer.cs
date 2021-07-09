using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearRenderer : MonoBehaviour
{
    public float rotationSpeed;
    public bool clockwise;

    private Quaternion _startRotation;
    private bool _started;

    public void Start()
    {
        _startRotation = transform.rotation;
        _started = true;
    }

    public void Update()
    {
        float angleDiff = rotationSpeed * Time.deltaTime;
        if (!clockwise) angleDiff *= 1;
        transform.Rotate(new Vector3(0, 0, angleDiff));
 
    }

    public void OnDisable()
    {
        transform.rotation = _startRotation;
    }

    public void OnEnable()
    {
        if (_started) Start();
    }
}
