using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneAttack : MonoBehaviour
{
    private bool returning = false;

    [HideInInspector] public LayerMask hitLayers;
    [HideInInspector] public Transform originPoint;
    [HideInInspector] public float range;
    [HideInInspector] public float length;
    [HideInInspector] public float duration;
    [HideInInspector] public int damage;
    [HideInInspector] public PlayerMovement _playerMovement;
    [HideInInspector] public bool playerRewinding;
    private float lifeBeforeRewind;
    [HideInInspector] public PlayerHealth _playerHealth;

    public ParticleSystem explosionParticles;


    public void RigToExplode()
    {
        Invoke("DetonateClone", this.duration);
        lifeBeforeRewind = _playerHealth.currentHealth;
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
        ParticleSystem explosion = Instantiate(explosionParticles, transform.position, transform.rotation);
        Destroy(explosion, 2.5f); // Note the particle itself should have a script to callback when it ends to destroy but time
        explosion.Play();

        // Detect enemies and doors in range of attack
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range, hitLayers);

        // Damage enemies and unlock doors
        foreach (Collider2D hit in hits)
        {
            //Debug.Log("Hit enemy!");
            if (hit.gameObject.layer == 8) // Hit enemy
                hit.GetComponent<EnemyBase>().Hit(damage);
            else
            { // Hit door
                DoorControl doorControl = hit.GetComponent<DoorControl>();
                if (doorControl.cloneAttackUnlocks)
                    doorControl.UnlockDoor();
            }
        }

        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        if (playerRewinding)
        { // Rewind player HP
            _playerHealth.currentHealth = Mathf.Max(_playerHealth.currentHealth, lifeBeforeRewind);
            _playerHealth.lifeBar.value = _playerHealth.currentHealth;
        }
    }

    public void SetSprite(Sprite sprite)
    {
        this.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
    }
}
