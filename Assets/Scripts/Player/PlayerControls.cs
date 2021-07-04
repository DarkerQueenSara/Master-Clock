using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private bool _jump = false;
    private bool _attack = false;
    private bool _extendedAttack = false;
    private bool _spinAttack = false;
    private bool _cloneAttack = false;

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
        _inputMaster.Player.Attack.canceled += _ => { _attack = false; };

        _inputMaster.Player.Extended_Attack.performed += ctx => { _extendedAttack = true; };
        _inputMaster.Player.Extended_Attack.canceled += _ => { _extendedAttack = false; };

        _inputMaster.Player.SpinAttack.performed += ctx => { _spinAttack = true; };
        _inputMaster.Player.SpinAttack.canceled += _ => { _spinAttack = false; };

        _inputMaster.Player.CloneAttack.performed += ctx => { _cloneAttack = true; };
        _inputMaster.Player.CloneAttack.canceled += _ => { _cloneAttack = false; };
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
        _playerCombat = GetComponent<PlayerCombat>();
    }

    private void Update()
    {
        if (_attack)
        {
            _playerCombat.SlashAttack();
        }
        else if (_extendedAttack)
        {
            _playerCombat.ExtendAttack();
        }
        else if (_spinAttack)
        {
            _playerCombat.SpinAttack();
        }
        else if (_cloneAttack)
        {
            _playerCombat.CloneAttack();
        }
    }

    private void FixedUpdate()
    {
        _playerMovement.Move(_directionInput.x, _jump, _directionInput.y < 0);
    }
}