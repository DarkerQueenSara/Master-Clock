using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovingPlatform : NonStaticPlatform
{
    public float range;

    public bool startLeft;

    private bool _goingRight;
    private float _startX;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        _goingRight = startLeft;
        _startX = transform.position.x;
        Vector3 dir = startLeft ? Vector3.left : Vector3.right;
        transform.position += dir * (range - 0.1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float currentX = transform.position.x;
        if ((currentX >= _startX + range && _goingRight) || (currentX <= _startX - range && !_goingRight))
        {
            _goingRight = !_goingRight;
        }

        Vector3 dir = _goingRight ? Vector3.right : Vector3.left;
        Vector2 targetVelocity = dir * moveSpeed * time.fixedDeltaTime ;
        body.velocity = Vector2.SmoothDamp(body.velocity, targetVelocity, ref velocity, movementSmoothing);    }
}