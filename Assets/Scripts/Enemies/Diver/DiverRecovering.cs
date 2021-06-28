using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;

public class DiverRecovering : EnemyState<Diver>
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
    }

    public override void StateFixedUpdate()
    {
        if (!_hit)
        {
            var currentPos = target.rb.position;
            target.rb.MovePosition(((Vector2) target.startPosition - currentPos).normalized *
                target.returnSpeed *
                Time.fixedDeltaTime + currentPos);
        }
        else
        {
            SetState(DiverHanging.Create(target));
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (target.hittables.HasLayer(col.gameObject.layer))
        {
            _hit = true;
        }
    }
}