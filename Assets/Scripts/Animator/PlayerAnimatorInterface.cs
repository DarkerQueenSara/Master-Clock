using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorInterface : MonoBehaviour
{
    public PlayerCombat _playerCombat;
    
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void ActivateHitbox(int index)
    {
        _playerCombat.ToggleHitbox(index,true);
    }

    public void DeactivateHitbox(int index)
    {
        _playerCombat.ToggleHitbox(index,false);
    }
}
