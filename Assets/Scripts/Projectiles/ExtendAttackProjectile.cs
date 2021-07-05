using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendAttackProjectile : MonoBehaviour
{
    private float speed = 0.0f;
    private bool returning = false;

    
    [HideInInspector] public LayerMask enemyLayer;
    [HideInInspector] public Transform originPoint;
    [HideInInspector] public float range;
    [HideInInspector] public float length;
    [HideInInspector] public float duration;
    [HideInInspector] public int damage;
    [HideInInspector] public PlayerMovement _playerMovement;

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(transform.position.x, originPoint.transform.position.y, transform.position.z); // Make sure bullet is always in same height as player
        
        if (!returning)
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
            
            if (_playerMovement._facingRight)
            {
                if (transform.position.x >= originPoint.position.x + length)
                {
                    returning = true;
                }
            }
            else
            {
                if (transform.position.x <= originPoint.position.x - length)
                {
                    returning = true;
                }
            }
        }
        else
        {
            transform.position -= new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);

            if (_playerMovement._facingRight)
            {
                if (transform.position.x <= originPoint.position.x)
                {
                    _playerMovement.moveBlocked = false;
                    Destroy(this.gameObject);
                }
            }
            else
            {
                if (transform.position.x >= originPoint.position.x)
                {
                    _playerMovement.moveBlocked = false;
                    Destroy(this.gameObject);
                }
            }

        }

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);

        // Damage enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            //Debug.Log("Hit enemy!");
            enemy.GetComponent<EnemyBase>().Hit(damage);
        }


    }

    public void SetSpeed()
    {
        // Set speed so that projectile reaches apex (i.e furthest away point) in half time
        //speed = distance / time
        speed = length / (duration * 0.5f);
        Debug.Log("Speed:" + speed);
        //Debug.Log("X: " + originPoint.position.x + length);

        if (!_playerMovement._facingRight)
            speed = -speed;
    }
}
