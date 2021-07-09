using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;

public class Swordfighter : EnemyBase<Swordfighter>
{
    public float moveSpeed;
    
    public float sightDistance;
    public float movementSmoothing = 0.05f;
    public float holdPositionTime;
    public float horizontalRange;

    public Transform attackPoint;
    public int attackDamage;
    public float attackRange;
    public float attackCooldown;
    
    public bool facingRight = true;

    public LayerMask groundMask;
    public LayerMask playerMask;

    public Transform wallCheck;
    public Transform groundCheck;

    //[HideInInspector] public SpriteRenderer capsuleSprite;
    
    public CapsuleCollider2D attackBox;
    [HideInInspector] public Rigidbody2D rb;

    [HideInInspector] public Vector2 currentPatrolAnchor;
    [HideInInspector] public Vector2 velocity;

    protected override void Start()
    {
        base.Start();
        if (!started)
        {
            rb = GetComponent<Rigidbody2D>();
            //capsuleSprite = GetComponent<SpriteRenderer>();
            state = SwordfighterIdle.Create(this);
            currentPatrolAnchor = transform.position;
            started = true;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (started)
        {
            state = SwordfighterIdle.Create(this);
            currentPatrolAnchor = transform.position;
        }
    }
    
}