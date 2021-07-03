using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendAttackProjectile : MonoBehaviour
{
    private float speed = 0.0f;
    private bool returning = false;

    [HideInInspector]
    public LayerMask enemyLayer;
    public Transform originPoint;
    public float range;
    public float length;
    public float duration;
    public int damage;
    public PlayerMovement _playerMovement;

    // Update is called once per frame
    void Update()
    {        
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
        speed = (originPoint.position.x + length) / (duration * 0.5f);

        if (!_playerMovement._facingRight)
            speed = -speed;
    }
}
