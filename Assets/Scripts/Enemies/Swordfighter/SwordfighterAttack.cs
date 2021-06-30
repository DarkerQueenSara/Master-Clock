using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordfighterAttack : SwordfighterState
{
    public static SwordfighterAttack Create(Swordfighter target)
    {
        SwordfighterAttack state = SwordfighterState.Create<SwordfighterAttack>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        //dar trigger da aniamção de ataque que devemos fazer como o player e 
        //dar tie da hitbox à animação
        target.rb.velocity = Vector2.zero;
        Debug.Log("Swordfighter attack");
        target.capsuleSprite.color = Color.magenta;
        Invoke(nameof(GoToNewState), target.attackCooldown);
    }

    private void GoToNewState()
    {
        if (CheckForPlayer())
        {
            target.capsuleSprite.color = Color.yellow;
            SetState(SwordfighterChase.Create(target));
        }
        else
        {
            target.capsuleSprite.color = Color.yellow;
            target.currentPatrolAnchor = transform.position;
            SetState(SwordfighterPatrol.Create(target));
        }
    }
}