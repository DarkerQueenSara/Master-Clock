using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diver : EnemyBase<Diver>
{
    public float diveRange;
    public float diveSpeed;
    public float returnSpeed;

    [HideInInspector] public Vector2 startPosition;
    //ground e player se calhar
    public LayerMask hittables;
    [HideInInspector] public Rigidbody2D rb;
    // Start is called before the first frame update
    
    private void Start()
    {
        base.Start();
        state = DiverHanging.Create(this);
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

}
