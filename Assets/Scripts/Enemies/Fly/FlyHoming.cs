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
       // transform.position += (Vector3) direction.normalized * target.flySpeed * Time.deltaTime;
       if (CheckIfFlip()) Flip();
       target.rb.position += direction.normalized * target.flySpeed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (target.playerMask.HasLayer(other.gameObject.layer))
        {
            Destroy(gameObject);
        }
    }
    
    private bool CheckIfFlip()
    {
        GameObject player = PlayerEntity.instance.gameObject;
        if (target.facingRight && player.transform.position.x < transform.position.x) return true;
        if (!target.facingRight && player.transform.position.x > transform.position.x) return true;
        return false;
    }
}