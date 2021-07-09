using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovingPlatform : NonStaticPlatform
{
    public float moveSpeed;
    public float range;

    public bool startLeft;

    private bool _goingRight;
    private float _startX;

    private bool _started;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        _goingRight = startLeft;
        _startX = transform.position.x;
        Vector3 dir = startLeft ? Vector3.left : Vector3.right;
        transform.position += dir * (range - 0.1f);
        _started = true;
    }

    // Update is called once per frame
    void Update()
    {
        float currentX = transform.position.x;
        if ((currentX >= _startX + range && _goingRight) || (currentX <= _startX - range && !_goingRight))
        {
            _goingRight = !_goingRight;
        }

        Vector3 dir = _goingRight ? Vector3.right : Vector3.left;
        transform.position += dir * Time.deltaTime * moveSpeed;
    }

    private void OnDisable()
    {
        transform.position = new Vector3(_startX, transform.position.y, transform.position.z);
    }

    private void OnEnable()
    {
        if (_started) Start();
    }

}