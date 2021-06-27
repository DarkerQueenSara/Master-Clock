using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private bool _jump = false;
    
    private InputMaster _inputMaster;
    private Vector2 _velocity;

    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _inputMaster = new InputMaster();
        _inputMaster.Player.Move.performed += ctx => { _velocity = ctx.ReadValue<Vector2>(); };
        _inputMaster.Player.Move.canceled += _ => { _velocity = Vector2.zero; };
        _inputMaster.Player.Jump.performed += ctx => { _jump = true; };
        _inputMaster.Player.Jump.canceled += _ => { _jump = false; };
    }

    private void OnEnable()
    {
        _inputMaster.Player.Enable();
    }

    private void OnDisable()
    {
        _inputMaster.Player.Disable();
    }

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }
    
    private void FixedUpdate()
    {
        _playerMovement.Move(_velocity.x, _jump);
    }
}