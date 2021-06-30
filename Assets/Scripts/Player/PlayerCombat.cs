using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    //0 = slash
    public List<GameObject> hitboxes;

    // Animator
    public Animator _animator;

    private void Start()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();
    }

    public void SlashAttack()
    {
        //dar trigger da animação slash attack
        _animator.SetTrigger("SlashAttack");
    }
    
    public void ToggleHitbox(int index, bool state)
    {
        hitboxes[index].SetActive(state);
    }
}