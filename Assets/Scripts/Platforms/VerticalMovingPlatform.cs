using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovingPlatform : MonoBehaviour
{
    public float moveSpeed;
    public float range;

    public bool startUp;

    private bool _goingDown;
    private float _startY;

    // Start is called before the first frame update
    void Start()
    {
        _goingDown = startUp;
        _startY = transform.position.y;
        Vector3 dir = startUp ? Vector3.up : Vector3.down;
        transform.position += dir * (range - 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        float currentY = transform.position.y;
        if ((currentY >= _startY + range && !_goingDown) || (currentY <= _startY - range && _goingDown))
        {
            _goingDown = !_goingDown;
        }

        Vector3 dir = _goingDown ? Vector3.down : Vector3.up;
        transform.position += dir * moveSpeed * Time.deltaTime;
    }
}