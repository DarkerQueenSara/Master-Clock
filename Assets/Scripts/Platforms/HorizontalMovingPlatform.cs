using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovingPlatform : MonoBehaviour
{
    
    public float moveSpeed;
    public float range;

    public bool startLeft;

    private bool _goingRight;
    private float _startX;    

    // Start is called before the first frame update
    void Start()
    {
        _goingRight = startLeft;
        _startX = transform.position.x;
        Vector3 dir = startLeft ? Vector3.left : Vector3.right;
        transform.position += dir * (range - 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        float currentX = transform.position.x;
        if (currentX >= _startX +  range || currentX <= _startX - range)
        {
            _goingRight = !_goingRight;
        }
        Vector3 dir = _goingRight ? Vector3.right : Vector3.left;
        transform.position += dir * moveSpeed * Time.deltaTime;
    }
}