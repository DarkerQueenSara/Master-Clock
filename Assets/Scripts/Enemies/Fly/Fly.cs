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
        if (!started)
        {
            state = FlyHoming.Create(this);
            rb = GetComponent<Rigidbody2D>();
            started = true;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (started) state = FlyHoming.Create(this);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Destroy(gameObject);
    }
}