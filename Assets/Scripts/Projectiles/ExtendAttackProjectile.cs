using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendAttackProjectile : MonoBehaviour
{
    private float speed = 0.0f;
    private bool returning = false;

    
    [HideInInspector] public LayerMask hitLayers;
    [HideInInspector] public Transform originPoint;
    [HideInInspector] public float range;
    [HideInInspector] public float length;
    [HideInInspector] public float duration;
    [HideInInspector] public int damage;
    [HideInInspector] public PlayerMovement _playerMovement;
    [HideInInspector] public bool _facingRight;

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(transform.position.x, originPoint.transform.position.y, transform.position.z); // Make sure bullet is always in same height as player
        
        if (!returning)
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
            
            if (_facingRight)
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

            if (_facingRight)
            {
                if (transform.position.x <= originPoint.position.x)
                {
                    if(_playerMovement != null)
                        _playerMovement.moveBlocked = false;
                    Destroy(this.gameObject);
                }
            }
            else
            {
                if (transform.position.x >= originPoint.position.x)
                {
                    if(_playerMovement != null)
                        _playerMovement.moveBlocked = false;
                    Destroy(this.gameObject);
                }
            }

        }

        // Detect enemies and doors in range of attack
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range, hitLayers);

        // Damage enemies and unlock doors
        foreach (Collider2D hit in hits)
        {
            //Debug.Log("Hit enemy!");
            if (hit.gameObject.layer == 8) // Hit enemy
                hit.GetComponent<EnemyBase>().Hit(damage);
            else if (hit.gameObject.layer == 6)
            { // Hit player
                hit.transform.parent.parent.GetComponent<PlayerHealth>().Hit(damage);
                break;
            }
            else
            { // Hit door
                DoorControl doorControl = hit.GetComponent<DoorControl>();
                if (doorControl.extendedAttackUnlocks)
                    doorControl.UnlockDoor();
            }
        }

    }

    public void SetSpeed()
    {
        // Set speed so that projectile reaches apex (i.e furthest away point) in half time
        //speed = distance / time
        speed = length / (duration * 0.5f);
        Debug.Log("Speed:" + speed);
        //Debug.Log("X: " + originPoint.position.x + length);

        if (!_facingRight)
            speed = -speed;
    }
}
