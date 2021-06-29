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
        if (CheckIfFlip()) Flip();
        //o player escapou
        if (HitHole() || HitWall())
        {
            target.currentPatrolAnchor = transform.position;
            target.rb.velocity = Vector2.zero;
            SetState(SwordfighterPatrol.Create(target));
        }
        else
        {
            GameObject player = PlayerEntity.instance.gameObject;
            //o player está em range de ataque
            if (InAttackRange())
            {
                SetState(SwordfighterAttacking.Create(target));
            }
            else
            {
                if (target.facingRight)
                {
                    MoveTowards(player.transform.position + Vector3.left * target.attackRange);
                }
                else
                {
                    MoveTowards(player.transform.position + Vector3.right * target.attackRange);
                }
            }
        }
    }

    private bool CheckIfFlip()
    {
        GameObject player = PlayerEntity.instance.gameObject;
        if (target.facingRight && player.transform.position.x < transform.position.x) return true;
        if (!target.facingRight && player.transform.position.x > transform.position.x) return true;
        return false;
    }

    private bool InAttackRange()
    {
        GameObject player = PlayerEntity.instance.gameObject;
        return Math.Abs(player.transform.position.x - transform.position.x) <= target.attackRange &&
               ((target.facingRight && player.transform.position.x > transform.position.x) ||
                (!target.facingRight && player.transform.position.x < transform.position.x)) &&
               Math.Abs(player.transform.position.y - transform.position.y) <= 1;
    }
}