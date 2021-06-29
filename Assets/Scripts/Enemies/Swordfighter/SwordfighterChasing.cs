using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordfighterChasing : SwordfighterState
{
    // Start is called before the first frame update
    public static SwordfighterChasing Create(Swordfighter target)
    {
        SwordfighterChasing state = SwordfighterState.Create<SwordfighterChasing>(target);
        return state;
    }

    // Update is called once per frame
    public override void StateUpdate()
    {
        if (!CheckForPlayer())
        {
            target.currentPatrolAnchor = transform.position.x;
            SetState(SwordfighterPatrol.Create(target));
        }
        else
        {
            float targetRange = target.attackBox.size.x;
            if (Math.Abs(PlayerEntity.instance.gameObject.transform.position.x - transform.position.x) <= target.attackRange)
            {
                SetState(SwordfighterAttacking.Create(target));
            }
        }
    }
}
