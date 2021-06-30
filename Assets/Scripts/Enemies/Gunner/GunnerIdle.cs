using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerIdle : GunnerState
{
    private float _cooldownLeft;

    public static GunnerIdle Create(Gunner target)
    {
        GunnerIdle state = GunnerState.Create<GunnerIdle>(target);
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
            SetState(GunnerChase.Create(target));
        }

        _cooldownLeft -= Time.deltaTime;
        if (_cooldownLeft <= 0) SetState(GunnerPatrol.Create(target));
    }
}
