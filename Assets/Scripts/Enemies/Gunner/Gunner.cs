using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : EnemyBase<Gunner>
{
    public int numberOfShots;
    public float fireRate;
    public float armRotateSpeed;

    public float moveSpeed;
    public float sightDistance;
    public float movementSmoothing = 0.05f;
    public float holdPositionTime;
    public float horizontalRange;
    public float horizontalAttackRange;
    public float verticalAttackRange;
    public float attackCooldown;

    public bool facingRight = true;

    public LayerMask groundMask;
    public LayerMask playerMask;

    public GameObject bulletPrefab;

    [HideInInspector] public SpriteRenderer capsuleSprite;

    public Transform armJoint;
    public Transform bulletSpawn;
    public Transform wallCheck;
    public Transform groundCheck;

    [HideInInspector] public Rigidbody2D rb;

    [HideInInspector] public Vector2 currentPatrolAnchor;
    [HideInInspector] public Vector2 velocity;

    protected override void Start()
    {
        base.Start();
        if (!started)
        {
            rb = GetComponent<Rigidbody2D>();
            state = GunnerIdle.Create(this);
            capsuleSprite = GetComponent<SpriteRenderer>();
            currentPatrolAnchor = transform.position;
            started = true;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (started)
        {
            state = GunnerIdle.Create(this);
            currentPatrolAnchor = transform.position;
        }
    }
}