using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerHealth _playerHealth;

    private void Start()
    {
        _playerHealth = transform.parent.GetComponent<PlayerHealth>();
    }

    private void OnCollisionEnter(Collision collision)
    {
       _playerHealth.CollisionDetected(collision.gameObject);
    }
}
