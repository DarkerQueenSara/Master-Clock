using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonStaticPlatform : MonoBehaviour
{
    private GameObject _player;
    private List<GameObject> _playerColliders;
    // Start is called before the first frame update
    void Start()
    {
        _player = PlayerEntity.instance.gameObject;
        _playerColliders = PlayerEntity.instance.colliders;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (_playerColliders.Contains(other.gameObject))
        {
            _player.transform.parent = transform;
        }   
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (_playerColliders.Contains(other.gameObject))
        {
            _player.transform.parent = null;
        } 
    }
}
