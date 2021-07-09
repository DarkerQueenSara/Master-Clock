using System;
using System.Collections;
using System.Collections.Generic;
using Chronos;
using UnityEngine;

public class PhysicsPlatform : NonStaticPlatform
{
    public float moveSpeed;
    public float movementSmoothing = 0.05f;
    
    protected Timeline time;
    protected RigidbodyTimeline2D body;
    protected Vector2 velocity;
    
    // Start is called before the first frame update
    public virtual void Start()
    {
        base.Start();
        time = GetComponent<Timeline>();
        if (time) body = time.rigidbody2D;
    }
    
}
