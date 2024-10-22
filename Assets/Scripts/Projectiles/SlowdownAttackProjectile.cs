using Chronos;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowdownAttackProjectile : MonoBehaviour
{
    private float speed = 0.0f;
    private bool returning = false;

    [HideInInspector] public LayerMask hitLayers;
    [HideInInspector] public Transform originPoint;
    [HideInInspector] public float radius;
    [HideInInspector] public float range;
    [HideInInspector] public float duration;
    [HideInInspector] public float forcefieldDuration;
    [HideInInspector] public int damage;
    [HideInInspector] public float slowdownAmount;
    //[HideInInspector] public PlayerMovement _playerMovement;
    [HideInInspector] public bool _facingRight;
    [HideInInspector] public GameObject slowdownForcefield;

    // Update is called once per frame
    void Update()
    {


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 2 || (collision.gameObject.layer == 6 && hitLayers != (hitLayers | (1 << 6))) || (collision.gameObject.layer == 8 && hitLayers != (hitLayers | (1 << 8))) )
        { // Ignore collisions with player (and player not in hit layers), enemy (and enemy not in hit layers) and ignoreraycast layers (rooms)
            return;
        }

        DetonateBomb();
    }

    public void RigToExplode()
    {
        Invoke("DetonateBomb", this.duration);
        //Destroy(this.gameObject, this.duration);
    }

    private void DetonateBomb()
    {
        // Play Explosion Animation
        Debug.Log("KABOOM");

        GameObject forcefieldInstance = Instantiate(slowdownForcefield, transform.position, Quaternion.identity); // Spawn forcefield on explosion point

        forcefieldInstance.transform.localScale = new Vector3(radius, radius, radius);
        forcefieldInstance.GetComponent<AreaClock2D>().localTimeScale = slowdownAmount;
        Destroy(forcefieldInstance, forcefieldDuration);

        // Detect enemies and doors in range of attack
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, hitLayers);

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
            }else
            { // Hit door
                DoorControl doorControl = hit.GetComponent<DoorControl>();
                if (doorControl.slowdownBombAttackUnlocks)
                    hit.GetComponent<DoorControl>().UnlockDoor();
            }
        }

        Destroy(this.gameObject);
    }


    public void SetParabolicVelocity()
    {
        var dir = _facingRight ? new Vector3(range, 0.0f, 0.0f) : new Vector3(-range, 0.0f, 0.0f); // Target Direction

        dir.y = 0; // We only need horizontal direction

        var dist = dir.magnitude; // Horizontal distance
        dir.y = dist; // Set elevation to 45 degrees

        var velocity = Mathf.Sqrt(dist * Physics2D.gravity.magnitude);
        this.gameObject.GetComponent<Rigidbody2D>().velocity = velocity * dir.normalized;

    }

}
