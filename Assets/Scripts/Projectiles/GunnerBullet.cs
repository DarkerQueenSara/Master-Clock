
using UnityEngine;

public class GunnerBullet : Bullet
{
    protected override void Hit(GameObject target)
    {
        base.Hit(target);
        //TODO meter a fazer dano para o player
        Destroy(gameObject);
    }
}
