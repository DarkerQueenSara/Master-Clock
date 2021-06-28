using System;
using UnityEngine;

public class Fly : EnemyBase<Fly>
{
    public float flySpeed;
    [HideInInspector] public Rigidbody2D rb;

    public LayerMask playerMask;
    
    protected override void Start()
    {
        base.Start();
        state = FlyHoming.Create(this);
        rb = GetComponent<Rigidbody2D>();
        started = true;
    }

    protected void OnDisable()
    {
        Destroy(gameObject);
    }
}