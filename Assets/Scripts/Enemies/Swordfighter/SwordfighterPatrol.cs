using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordfighterPatrol : SwordfighterState
{
    private Vector2 _patrolLeftPoint;
    private Vector2 _patrolRightPoint;

    public static SwordfighterPatrol Create(Swordfighter target)
    {
        SwordfighterPatrol state = SwordfighterState.Create<SwordfighterPatrol>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        _patrolLeftPoint = (Vector2) target.currentPatrolAnchor + Vector2.left * target.horizontalRange;
        _patrolRightPoint = (Vector2) target.currentPatrolAnchor + Vector2.right * target.horizontalRange;
        Flip();
    }

    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();
        if (target.facingRight)
        {
            MoveTowards(_patrolRightPoint);
        }
        else
        {
            MoveTowards(_patrolLeftPoint);
        }

        if (CheckForPlayer())
        {
            SetState(SwordfighterChasing.Create(target));
        }

        if (HitWall() || TimeToChange())
        {
            SetState(SwordfighterIdle.Create(target));
        }
    }

    private bool TimeToChange()
    {
        if (!target.facingRight && Math.Abs(transform.position.x - _patrolLeftPoint.x) <= 0.05f) return true;
        if (target.facingRight && Math.Abs(transform.position.x - _patrolRightPoint.x) <= 0.05f) return true;
        return false;
    }
}