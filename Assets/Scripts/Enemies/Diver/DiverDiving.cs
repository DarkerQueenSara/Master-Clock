using System;
using Extensions;
using UnityEngine;

public class DiverDiving : DiverState
{
    private bool _hit = false;
    private float _diveCoordinates;

    public static DiverDiving Create(Diver target)
    {
        DiverDiving state = DiverState.Create<DiverDiving>(target);
        return state;
    }

    // Start is called before the first frame update
    public override void StateStart()
    {
        base.StateStart();
        _hit = false;
        _diveCoordinates = PlayerEntity.instance.transform.position.x;
        animator.SetBool("Diving", true);
        animator.SetBool("Idle", false);
        audioManager.Play("Diving");
    }

    public override void StateFixedUpdate()
    {
        if (!_hit)
        {
            Vector3 currentPos = target.rb.position;
            transform.position += new Vector3(_diveCoordinates - currentPos.x, -1, 0).normalized * target.diveSpeed *
                                  Time.fixedDeltaTime;
        }
        else
        {
            audioManager.Stop("Diving");
            SetState(DiverRecovering.Create(target));
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