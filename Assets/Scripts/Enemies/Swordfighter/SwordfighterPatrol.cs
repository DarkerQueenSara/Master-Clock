using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordfighterPatrol : SwordfighterState
{
    
    private Vector3 _patrolStartPosition;
    
    public static SwordfighterPatrol Create(Swordfighter target)
    {
        SwordfighterPatrol state = SwordfighterState.Create<SwordfighterPatrol>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        _patrolStartPosition = transform.position;
        Flip();
    }

    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();
        if (target.facingRight)
        {
            MoveTowards(_patrolStartPosition + Vector3.right * target.horizontalRange);
        }
        else
        {
            MoveTowards(_patrolStartPosition + Vector3.left * target.horizontalRange);
        }

        if (CheckForPlayer())
        {
            SetState(SwordfighterChasing.Create(target));
        }
        if (HitWall() || Math.Abs(transform.position.x - _patrolStartPosition.x) >= target.horizontalRange)
        {
            SetState(SwordfighterIdle.Create(target));
        }
        
    }
}
