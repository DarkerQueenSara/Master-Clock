using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crawler : EnemyBase<Crawler>
{
    public float moveSpeed;
    public float rayDistance = 0.1f;
    public Transform head;
    public Transform wallFinder;

    public LayerMask groundMask;
    [HideInInspector] public BoxCollider2D boxCollider;
    [HideInInspector] public Rigidbody2D rb;

    protected override void Start()
    {
        base.Start();
        if (!started)
        {
            state = CrawlerCrawling.Create(this);
            boxCollider = GetComponent<BoxCollider2D>();
            rb = GetComponent<Rigidbody2D>();
            started = true;
        }            
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (started) state = CrawlerCrawling.Create(this);
    }
}