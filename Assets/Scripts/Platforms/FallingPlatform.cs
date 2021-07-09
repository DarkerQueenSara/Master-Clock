using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : PhysicsPlatform
{
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 targetVelocity = Vector3.down * moveSpeed * time.fixedDeltaTime ;
        body.velocity = Vector2.SmoothDamp(body.velocity, targetVelocity, ref velocity, movementSmoothing);
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }
}
