using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private bool _jump = false;
    private bool _attack = false;
    
    private InputMaster _inputMaster;
    private Vector2 _directionInput;

    private PlayerMovement _playerMovement;
    private PlayerCombat _playerCombat;
    
    private void Awake()
    {
        _inputMaster = new InputMaster();
        _inputMaster.Player.Move.performed += ctx => { _directionInput = ctx.ReadValue<Vector2>(); };
        _inputMaster.Player.Move.canceled += _ => { _directionInput = Vector2.zero; };
        _inputMaster.Player.Jump.performed += ctx => { _jump = true; };
        _inputMaster.Player.Jump.canceled += _ => { _jump = false; };
        _inputMaster.Player.Attack.performed += ctx => { _attack = true; };
        _inputMaster.Player.Attack.canceled += _ => { _attack = false; };    }

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
        _playerCombat = GetComponent<PlayerCombat>();
    }

    private void Update()
    {
        if (_attack)
        {
            _playerCombat.SlashAttack();
        }
    }

    private void FixedUpdate()
    {
        _playerMovement.Move(_directionInput.x, _jump, _directionInput.y < 0);
    }
}