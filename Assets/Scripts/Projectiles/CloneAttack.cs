using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneAttack : MonoBehaviour
{
    private bool returning = false;

    [HideInInspector]
    public LayerMask enemyLayer;
    public Transform originPoint;
    public float range;
    public float length;
    public float duration;
    public int damage;
    public PlayerMovement _playerMovement;   

    public void RigToExplode()
    {
        Destroy(this.gameObject, this.duration);
    }

    public void ReturnToClone()
    {
    }

    private void OnDestroy()
    {
        // Play Explosion Animation
        Debug.Log("KABOOM");

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);

        // Damage enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            //Debug.Log("Hit enemy!");
            enemy.GetComponent<EnemyBase>().Hit(damage);
        }
    }
}
