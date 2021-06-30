using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerAttack : GunnerState
{
    private int _shotsFired;
    private float _bulletCooldown;

    public static GunnerAttack Create(Gunner target)
    {
        GunnerAttack state = GunnerState.Create<GunnerAttack>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        _bulletCooldown = target.fireRate;
        target.rb.velocity = Vector2.zero;
        target.capsuleSprite.color = Color.magenta;
    }

    public override void StateUpdate()
    {
        Vector3 playerPos = PlayerEntity.instance.gameObject.transform.position;
        Vector3 ourPos = target.bulletSpawn.transform.position;
        Vector3 direction = (playerPos - ourPos);

        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        target.armJoint.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

        _bulletCooldown -= Time.deltaTime;
        if (_bulletCooldown <= 0 && _shotsFired < target.numberOfShots)
        {
            var bullet = Instantiate(target.bulletPrefab, target.bulletSpawn.position, Quaternion.identity)
                .GetComponent<Bullet>();
            bullet.Init(direction.normalized);
            _shotsFired++;
            _bulletCooldown = target.fireRate;
        }

        if (_shotsFired >= target.numberOfShots)
        {
            GoToNewState();
        }
    }

    private void GoToNewState()
    {
        if (CheckForPlayer())
        {
            target.capsuleSprite.color = Color.green;
            SetState(GunnerChase.Create(target));
        }
        else
        {
            target.capsuleSprite.color = Color.green;
            target.currentPatrolAnchor = transform.position;
            SetState(GunnerPatrol.Create(target));
        }
    }
}