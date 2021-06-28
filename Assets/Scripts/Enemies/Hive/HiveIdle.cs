using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveIdle : HiveState
{
    private float _cooldownLeft;

    public static HiveIdle Create(Hive target)
    {
        HiveIdle state = HiveState.Create<HiveIdle>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        _cooldownLeft = target.cooldown;
    }

    public override void StateUpdate()
    {
        _cooldownLeft -= Time.deltaTime;
        if (_cooldownLeft <= 0)
        {
            SetState(HiveSpawning.Create(target));
        }
    }
}