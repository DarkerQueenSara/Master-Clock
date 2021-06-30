using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerChase : GunnerState
{
    // Start is called before the first frame update
    public static GunnerChase Create(Gunner target)
    {
        GunnerChase state = GunnerState.Create<GunnerChase>(target);
        return state;
    }

    // Update is called once per frame
    public override void StateUpdate()
    {
        if (CheckIfFlip()) Flip();
        //o player escapou
        if (HitHole() || HitWall())
        {
            target.rb.velocity = Vector2.zero;
            if (InAttackRange())
            {
                SetState(GunnerAttack.Create(target));
            }
            else
            {
                target.currentPatrolAnchor = transform.position;
                SetState(GunnerPatrol.Create(target));
            }
        }
        else
        {
            //o player está em range de ataque
            if (InAttackRange())
            {
                SetState(GunnerAttack.Create(target));
            }
            else
            {
                //nao preciso de calculos complicados com o player, só tenho de ir para o patrol point mais perto
                if (target.facingRight)
                {
                    MoveInDirection(Vector2.right);
                }
                else
                {
                    MoveInDirection(Vector2.left);
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
        Vector3 player = PlayerEntity.instance.gameObject.transform.position;
        Vector3 pos = transform.position;
        return Math.Abs(player.x - pos.x) <= target.horizontalAttackRange &&
               ((target.facingRight && player.x > pos.x) ||
                (!target.facingRight && player.x < pos.x)) &&
               Math.Abs(player.y - pos.y) <= target.verticalAttackRange;
    }
}