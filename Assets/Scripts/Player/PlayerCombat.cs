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

    private PlayerMovement _playerMovement;

    public LayerMask enemyLayer;

    private float timeUntilNextAttack = 0.0f;

    [Header("Slash")]
    public int slashAttackDamage;
    public Transform slashAttackPoint;
    public float slashAttackRange;
    public float slashAttackDuration;

    [Header("Extend")]
    public int extendAttackDamage;
    public GameObject extendAttackProjectile;
    public Transform extendAttackPoint;
    public float extendAttackRange;
    public float extendAttackLength;
    public float extendAttackDuration;

    private void Start()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();
        _playerMovement = this.gameObject.GetComponent<PlayerMovement>();
    }

    public void Update()
    {
        if (timeUntilNextAttack > 0.0f)
        {
            timeUntilNextAttack = Mathf.Max(0.0f, timeUntilNextAttack - Time.deltaTime);
        }

    }

    public void SlashAttack()
    {
        if (timeUntilNextAttack > 0.0f)
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

        timeUntilNextAttack = slashAttackDuration;
    }

    public void ExtendAttack()
    {
        if (timeUntilNextAttack > 0.0f)
        {
            return;
        }

        // Play attack animation
        //_animator.SetTrigger("SlashAttack");

        // Create the projectile that will act as the yoyo
        GameObject projectile = Instantiate(extendAttackProjectile, extendAttackPoint.position, Quaternion.identity);

        ExtendAttackProjectile projectileScript = projectile.GetComponent<ExtendAttackProjectile>();
        projectileScript.enemyLayer = enemyLayer;
        projectileScript.originPoint = extendAttackPoint;
        projectileScript.damage = extendAttackDamage;
        projectileScript.range = extendAttackRange;
        projectileScript.length = extendAttackLength;
        projectileScript.duration = extendAttackDuration;
        projectileScript._playerMovement = _playerMovement;

        projectileScript.SetSpeed();

        // PREVENT PLAYER FROM MOVING WHEN USING THIS ATTACK UNTIL IT IS DONE
        _playerMovement.moveBlocked = true;
        _playerMovement.StopPlayer();

        timeUntilNextAttack = extendAttackDuration;
    }

    private void OnDrawGizmosSelected()
    {
        //Gizmos.DrawWireSphere(slashAttackPoint.position, slashAttackRange);
        Gizmos.DrawWireSphere(extendAttackPoint.position, extendAttackRange);
        Gizmos.DrawWireSphere(extendAttackPoint.position + Vector3.right * extendAttackLength, extendAttackRange);
    }

    public void ToggleHitbox(int index, bool state)
    {
        //hitboxes[index].SetActive(state);
    }
}