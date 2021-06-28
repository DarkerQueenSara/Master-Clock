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
        state = DiverHanging.Create(this);
        rb = GetComponent<Rigidbody2D>();
        started = true;
    }
    
}
