using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private bool _jump = false;
    private bool _attack = false;
    private bool _accelerate = false;
    private bool _extendedAttack = false;
    private bool _slowdownAttack = false;
    private bool _spinAttack = false;
    private bool _cloneAttack = false;

    public bool _accelerate_unlocked = false;
    public bool _extendedAttack_unlocked = false;
    public bool _slowdownAttack_unlocked = false;
    public bool _spinAttack_unlocked = false;
    public bool _cloneAttack_unlocked = false;

    public GameObject _accelerate_ui;
    public GameObject _extendedAttack_ui;
    public GameObject _slowdownAttack_ui;
    public GameObject _spinAttack_ui;
    public GameObject _cloneAttack_ui;

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

        _inputMaster.Player.AccelerateTime.performed += ctx => { _accelerate = true; };
        _inputMaster.Player.AccelerateTime.canceled += _ => { _accelerate = false; _playerCombat.Deaccelerate(); };

        _inputMaster.Player.Attack.performed += ctx => { _attack = true; };
        _inputMaster.Player.Attack.canceled += _ => { _attack = false; };

        _inputMaster.Player.Extended_Attack.performed += ctx => { _extendedAttack = true; };
        _inputMaster.Player.Extended_Attack.canceled += _ => { _extendedAttack = false; };

        _inputMaster.Player.SlowdownBomb.performed += ctx => { _slowdownAttack = true; };
        _inputMaster.Player.SlowdownBomb.canceled += _ => { _slowdownAttack = false; };

        _inputMaster.Player.SpinAttack.performed += ctx => { _spinAttack = true; };
        _inputMaster.Player.SpinAttack.canceled += _ => { _spinAttack = false;};

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
        if (_accelerate && _accelerate_unlocked)
        {
            _playerCombat.Accelerate();
        }

        if (_attack)
        {
            _attack = false;
            _playerCombat.SlashAttack();
        }
        else if (_extendedAttack && _extendedAttack_unlocked)
        {
            _extendedAttack = false;
            _playerCombat.ExtendAttack();
        }
        else if (_spinAttack && _spinAttack_unlocked)
        {
            _spinAttack = false;
            _playerCombat.SpinAttack();
        }
        else if (_cloneAttack && _cloneAttack_unlocked)
        {
            _cloneAttack = false;
            _playerCombat.CloneAttack();
        }else if (_slowdownAttack && _slowdownAttack_unlocked)
        {
            _slowdownAttack = false;
            _playerCombat.slowdownAttack();
        }
    }

    private void FixedUpdate()
    {
        _playerMovement.Move(_directionInput.x, _jump, _directionInput.y < 0);
    }


    public void UnlockPowerup(string powerup_name)
    {
        switch (powerup_name)
        { // NOTE: These should be types instead of strings to avoid problems, but meh
            case "extended_attack":
                _extendedAttack_unlocked = true;
                if (_extendedAttack_ui != null)
                    _extendedAttack_ui.SetActive(true);
                break;
            case "slowdown_bomb_attack":
                _slowdownAttack_unlocked = true;
                if (_slowdownAttack_ui != null)
                    _slowdownAttack_ui.SetActive(true);
                break;
            case "clone_attack":
                _cloneAttack_unlocked = true;
                if (_cloneAttack_ui != null)
                    _cloneAttack_ui.SetActive(true);
                break;
            case "spin_attack":
                _spinAttack_unlocked = true;
                if (_spinAttack_ui != null)
                    _spinAttack_ui.SetActive(true);
                break;
            case "accelerate_attack":
                _accelerate_unlocked = true;
                if (_accelerate_ui != null)
                    _accelerate_ui.SetActive(true);
                break;
            default:
                break;
        }
    }
}