using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordfighterAttacking : SwordfighterState
{
    public static SwordfighterAttacking Create(Swordfighter target)
    {
        SwordfighterAttacking state = SwordfighterState.Create<SwordfighterAttacking>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        //dar trigger da aniamção de ataque que devemos fazer como o player e 
        //dar tie da hitbox à animação
        Debug.Log("Swordfighter attack");
        if (CheckForPlayer())
        {
            SetState(SwordfighterChasing.Create(target));
        }
        else
        {
            target.currentPatrolAnchor = transform.position.x;
            SetState(SwordfighterPatrol.Create(target));
        }
    }
}
