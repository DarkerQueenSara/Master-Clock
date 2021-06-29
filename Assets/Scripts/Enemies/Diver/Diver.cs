using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diver : EnemyBase<Diver>
{
    public float diveRange;
    public float diveSpeed;
    public float returnSpeed;

    public LayerMask hittables;
    [HideInInspector] public Rigidbody2D rb;

    protected override void Start()
    {
        base.Start();
        if (!started)
        {
            rb = GetComponent<Rigidbody2D>();
            state = DiverHanging.Create(this);
            started = true;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (started) state = DiverHanging.Create(this);
    }
}