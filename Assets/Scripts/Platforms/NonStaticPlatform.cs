using System;
using System.Collections;
using System.Collections.Generic;
using Chronos;
using UnityEngine;

public class NonStaticPlatform : MonoBehaviour
{
    private GameObject _player;
    private List<GameObject> _playerColliders;

    private Rigidbody2D _playerRb;
    
    public float moveSpeed;
    public float movementSmoothing = 0.05f;
    
    private float _defaultMoveSpeed;
    
    protected Timeline time;
    protected RigidbodyTimeline2D body;
    protected Vector2 velocity;
    
    private bool _onPlat;
    
    // Start is called before the first frame update
    public virtual void Start()
    {
        _player = PlayerEntity.instance.gameObject;
        _playerColliders = PlayerEntity.instance.colliders;
        _playerRb = _player.GetComponent<Rigidbody2D>();
        _defaultMoveSpeed = moveSpeed;
        time = GetComponent<Timeline>();
        body = time.rigidbody2D;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (_playerColliders.Contains(other.gameObject))
        {
            _player.transform.SetParent(gameObject.transform,true);
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
