
using UnityEngine;

public class GunnerBullet : Bullet
{
    public int bulletDamage = 60;
    protected override void Hit(GameObject target)
    {
        base.Hit(target);
        PlayerEntity.instance.health.Hit(bulletDamage);
        Destroy(gameObject);
    }
}
