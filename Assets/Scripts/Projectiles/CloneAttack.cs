using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneAttack : MonoBehaviour
{
    private bool returning = false;

    [HideInInspector] public LayerMask enemyLayer;
    [HideInInspector] public Transform originPoint;
    [HideInInspector] public float range;
    [HideInInspector] public float length;
    [HideInInspector] public float duration;
    [HideInInspector] public int damage;
    [HideInInspector] public PlayerMovement _playerMovement;
    [HideInInspector] public bool playerRewinding;


    public void RigToExplode()
    {
        Invoke("DetonateClone", this.duration);
        //Destroy(this.gameObject, this.duration);
    }

    private void DetonateClone()
    {
        if (playerRewinding)
        { // If player is rewinding, don't destroy the clone
            return;
        }

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

        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        
    }
}
