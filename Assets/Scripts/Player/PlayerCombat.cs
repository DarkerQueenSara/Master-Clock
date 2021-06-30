using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    //0 = slash
    //public List<GameObject> hitboxes;

    // Animator
    private Animator _animator;

    public LayerMask enemyLayer;

    private float nextAttackTime = 0.0f;

    [Header("Slash")]
    public int slashAttackDamage;
    public Transform slashAttackPoint;
    public float slashAttackRange;
    public float slashAttackDuration;

    private void Start()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();
    }

    public void SlashAttack()
    {
        if(Time.time < nextAttackTime)
        {
            return;
        }

        // Play attack animation
        _animator.SetTrigger("SlashAttack");

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(slashAttackPoint.position, slashAttackRange, enemyLayer);

        // Damage enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            //Debug.Log("Hit enemy!");
            enemy.GetComponent<EnemyBase>().Hit(slashAttackDamage);
        }

        nextAttackTime = Time.time + 1.0f / slashAttackDuration;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(slashAttackPoint.position, slashAttackRange);
    }

    public void ToggleHitbox(int index, bool state)
    {
        //hitboxes[index].SetActive(state);
    }
}