using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public LayerMask enemyLayer;

    public int attackDamage = 1;

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Swoosh");
        if(collision.gameObject.layer == enemyLayer) // If collided with an enemy
        {
            Debug.Log("Hit enemy!");
        }
    }
    */
}
