using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;

public class FlyHoming : FlyState
{
    public static FlyHoming Create(Fly target)
    {
        FlyHoming state = FlyState.Create<FlyHoming>(target);
        return state;
    }

    public override void StateFixedUpdate()
    {
        var currentPos = target.rb.position;
        var direction = (Vector2) PlayerEntity.instance.transform.position - currentPos;
        transform.position += (Vector3) direction.normalized * target.flySpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (target.playerMask.HasLayer(other.gameObject.layer))
        {
            Destroy(gameObject);
        }
    }
}