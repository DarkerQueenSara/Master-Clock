using System;
using UnityEngine;

public class SwordfighterIdle : SwordfighterState
{
    private float _cooldownLeft;

    public static SwordfighterIdle Create(Swordfighter target)
    {
        SwordfighterIdle state = SwordfighterState.Create<SwordfighterIdle>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        _cooldownLeft = target.holdPositionTime;
        target.rb.velocity = Vector2.zero;
    }

    public override void StateUpdate()
    {
        if (CheckForPlayer())
        {
            SetState(SwordfighterChase.Create(target));
        }

        _cooldownLeft -= Time.deltaTime;
        if (_cooldownLeft <= 0) SetState(SwordfighterPatrol.Create(target));
    }
}