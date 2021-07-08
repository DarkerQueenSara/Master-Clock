using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public float spawnRate;
    private float _spawnCooldown;

    

    private void Update()
    {
        _spawnCooldown -= Time.deltaTime;
        if (_spawnCooldown <= 0)
        {
            Instantiate(platformPrefab, transform.position, Quaternion.identity, transform.parent.gameObject.transform);
            _spawnCooldown = spawnRate;
        }
    }
}