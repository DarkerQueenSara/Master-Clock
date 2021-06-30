using System;
using UnityEngine;

public class SwordfighterChase : SwordfighterState
{
    // Start is called before the first frame update
    public static SwordfighterChase Create(Swordfighter target)
    {
        SwordfighterChase state = SwordfighterState.Create<SwordfighterChase>(target);
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
                SetState(SwordfighterAttack.Create(target));
            }
            else
            {
                target.currentPatrolAnchor = transform.position;
                SetState(SwordfighterPatrol.Create(target));
            }
        }
        else
        {
            GameObject player = PlayerEntity.instance.gameObject;
            //o player está em range de ataque
            if (InAttackRange())
            {
                SetState(SwordfighterAttack.Create(target));
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
        GameObject player = PlayerEntity.instance.gameObject;
        return Math.Abs(player.transform.position.x - transform.position.x) <= target.attackRange &&
               ((target.facingRight && player.transform.position.x > transform.position.x) ||
                (!target.facingRight && player.transform.position.x < transform.position.x)) &&
               Math.Abs(player.transform.position.y - transform.position.y) <= 1;
    }
}