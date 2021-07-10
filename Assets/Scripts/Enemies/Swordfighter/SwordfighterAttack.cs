using Extensions;
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
        
        animator.SetTrigger("Attack");
        audioManager.Play("Attack");
        target.rb.velocity = Vector2.zero;

        // Detect enemies and doors in range of attack
        Collider2D[] hits =
            Physics2D.OverlapCircleAll(target.attackPoint.position, target.attackRange, target.playerMask);

        // Damage enemies and unlock doors
        foreach (Collider2D hit in hits)
        {
            //Debug.Log("Hit enemy!");
            if (target.playerMask.HasLayer(hit.gameObject.layer)) // Hit enemy
                PlayerEntity.instance.health.Hit(target.attackDamage);
        }

        //target.rb.velocity = Vector2.zero;
        //Debug.Log("Swordfighter attack");
        //target.capsuleSprite.color = Color.magenta;
        Invoke(nameof(GoToNewState), target.attackCooldown);
    }

    private void GoToNewState()
    {
        if (CheckForPlayer())
        {
            //target.capsuleSprite.color = Color.yellow;
            SetState(SwordfighterChase.Create(target));
        }
        else
        {
            //target.capsuleSprite.color = Color.yellow;
            target.currentPatrolAnchor = transform.position;
            SetState(SwordfighterPatrol.Create(target));
        }
    }
}