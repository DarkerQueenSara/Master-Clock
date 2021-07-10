using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;

public class DiverRecovering : DiverState
{
    private bool _hit = false;

    public static DiverRecovering Create(Diver target)
    {
        DiverRecovering state = DiverState.Create<DiverRecovering>(target);
        return state;
    }

    // Start is called before the first frame update
    public override void StateStart()
    {
        base.StateStart();
        _hit = false;
        animator.SetBool("Diving", false);
        animator.SetBool("Returning", true);
    }

    public override void StateFixedUpdate()
    {
        if (!_hit)
        {
            Vector3 currentPos = target.rb.position;
            transform.position += ((Vector3) target.startPosition - currentPos).normalized *
                                  target.returnSpeed *
                                  Time.fixedDeltaTime;
        }
        else
        {
            SetState(DiverHanging.Create(target));
        }
    }
    
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (target.hittables.HasLayer(other.gameObject.layer))
        {
            _hit = true;
        }
    }
}