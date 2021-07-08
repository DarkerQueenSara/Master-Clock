using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiverHanging : DiverState
{
    public static DiverHanging Create(Diver target)
    {
        DiverHanging state = DiverState.Create<DiverHanging>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        _animator.SetBool("Idle", true);
        _animator.SetBool("Returning", false);
    }
    
    public override void StateUpdate()
    {
        PlayerEntity player = PlayerEntity.instance;
        if (Mathf.Abs(transform.position.x - player.transform.position.x) < target.diveRange &&
            transform.position.y > player.transform.position.y)
        {
            SetState(DiverDiving.Create(target));
        }
    }
}