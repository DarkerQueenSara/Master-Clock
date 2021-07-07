using Chronos;
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

    public LayerMask hitLayers;


    private float timeUntilNextAttack = 0.0f;

    // Time Stuff
    private Timeline _time;
    private Clock playerClock;
    private Clock globalClock;
    private Clock enemyClock;


    [Header("Slash")]
    public int slashAttackDamage;
    public Transform slashAttackPoint;
    public float slashAttackRange;
    public float slashAttackDuration;

    [Header("Spin")]
    public int spinAttackDamage;
    public Transform spinAttackPoint;
    public Vector2 spinAttackRange;
    public float spinAttackDuration;

    [Header("Extend")]
    public int extendAttackDamage;
    public GameObject extendAttackProjectile;
    public Transform extendAttackPoint;
    public float extendAttackRange;
    public float extendAttackLength;
    public float extendAttackDuration;

    [Header("Clone")]
    public int cloneAttackDamage;
    public GameObject clone;
    public Transform cloneAttackPoint;
    public float cloneAttackRange;
    public float cloneAttackDuration;
    private float currentTime = 0.0f;
    private float timeUntilNextCloneAttack = 0.0f;

    [Header("Slowdown")]
    public int slowdownAttackDamage;
    public GameObject slowdownAttackProjectile;
    public Transform slowdownAttackPoint;
    public float slowdownAttackRadius;
    public float slowdownAttackSlowdownAmount;
    public float slowdownThrowRange;
    public float slowdownAttackDuration;
    public float slowdownForcefieldDuration;
    public GameObject slowdownForcefield;

    [Header("Accelerate")]
    public float playerAcceleration;
    public float globalAcceleration;
    public float enemyAcceleration;

    private void Start()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();
        _playerMovement = this.gameObject.GetComponent<PlayerMovement>();
        _time = GetComponent<Timeline>();

        playerClock = Timekeeper.instance.Clock("Player");
        globalClock = Timekeeper.instance.Clock("Global");
        enemyClock = Timekeeper.instance.Clock("Enemy");
    }

    public void Update()
    {
        if (timeUntilNextAttack > 0.0f)
        {
            timeUntilNextAttack = Mathf.Max(0.0f, timeUntilNextAttack - Time.deltaTime);
        }
        if (timeUntilNextCloneAttack > 0.0f)
        {
            timeUntilNextCloneAttack = Mathf.Max(0.0f, timeUntilNextAttack - Time.deltaTime);
        }
        
        if (playerClock.localTimeScale < 0.0f)
        { // Rewinding to clone
            currentTime += playerClock.deltaTime;

            if (currentTime <= 0.0f)
            {
                playerClock.localTimeScale = 1.0f;
                GameObject cloneInstance = GameObject.FindGameObjectWithTag("Clone");
                Destroy(cloneInstance, 0.0f);
            }
        }
        else
        { // Count time since clone was spawned
            currentTime += playerClock.deltaTime;
        }
    }

    public void SlashAttack()
    {
        if (timeUntilNextAttack > 0.0f || _time.timeScale <= 0)
        {
            return;
        }

        // TODO: Play attack animation
        _animator.SetTrigger("SlashAttack");

        // Detect enemies and doors in range of attack
        Collider2D[] hits = Physics2D.OverlapCircleAll(slashAttackPoint.position, slashAttackRange, hitLayers);

        // Damage enemies and unlock doors
        foreach (Collider2D hit in hits)
        {
            //Debug.Log("Hit enemy!");
            if (hit.gameObject.layer == 8) // Hit enemy
                hit.GetComponent<EnemyBase>().Hit(slashAttackDamage);
            else
            { // Hit door
                DoorControl doorControl = hit.GetComponent<DoorControl>();
                if(doorControl.normalAttackUnlocks)
                    hit.GetComponent<DoorControl>().UnlockDoor();
            }
        }

        timeUntilNextAttack = slashAttackDuration;
    }

    public void ExtendAttack()
    {
        if (timeUntilNextAttack > 0.0f || _time.timeScale <= 0)
        {
            return;
        }

        // Play attack animation
        //_animator.SetTrigger("SlashAttack");

        // Create the projectile that will act as the yoyo
        GameObject projectile = Instantiate(extendAttackProjectile, extendAttackPoint);
        //GameObject projectile = Instantiate(extendAttackProjectile, extendAttackPoint.position, Quaternion.identity);


        ExtendAttackProjectile projectileScript = projectile.GetComponent<ExtendAttackProjectile>();
        projectileScript.hitLayers = hitLayers;
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

    public void slowdownAttack()
    {
        if (timeUntilNextAttack > 0.0f || _time.timeScale <= 0)
        {
            return;
        }

        // Play attack animation
        //_animator.SetTrigger("SlashAttack");

        // Create the projectile that will act as the yoyo
        GameObject projectile = Instantiate(slowdownAttackProjectile, slowdownAttackPoint.position, this.gameObject.transform.rotation);
        //GameObject projectile = Instantiate(extendAttackProjectile, extendAttackPoint.position, Quaternion.identity);


        SlowdownAttackProjectile projectileScript = projectile.GetComponent<SlowdownAttackProjectile>();
        projectileScript.hitLayers = hitLayers;
        projectileScript.originPoint = slowdownAttackPoint;
        projectileScript.damage = slowdownAttackDamage;
        projectileScript.radius = slowdownAttackRadius;
        projectileScript.range = slowdownThrowRange;
        projectileScript.duration = slowdownAttackDuration;
        projectileScript.slowdownAmount = slowdownAttackSlowdownAmount;
        projectileScript._playerMovement = _playerMovement;

        projectileScript.slowdownForcefield = slowdownForcefield;
        projectileScript.forcefieldDuration = slowdownForcefieldDuration;


        projectileScript.SetParabolicVelocity();
        projectileScript.RigToExplode();

        timeUntilNextAttack = slowdownAttackDuration;
    }


    public void SpinAttack()
    {
        if (timeUntilNextAttack > 0.0f || _time.timeScale <= 0)
        {
            return;
        }

        // TODO: Play attack animation
        Debug.Log("Spinnin!");
        //_animator.SetTrigger("SpinAttack");


        // Detect enemies and doors in range of attack
        Collider2D[] hits = Physics2D.OverlapBoxAll(spinAttackPoint.position, spinAttackRange, 0.0f, hitLayers);

        // Damage enemies and unlock doors
        foreach (Collider2D hit in hits)
        {
            //Debug.Log("Hit enemy!");
            if (hit.gameObject.layer == 8) // Hit enemy
                hit.GetComponent<EnemyBase>().Hit(spinAttackDamage);
            else
            { // Hit door
                DoorControl doorControl = hit.GetComponent<DoorControl>();
                if (doorControl.spinAttackUnlocks)
                    hit.GetComponent<DoorControl>().UnlockDoor();
            }

            Debug.Log("HIT ENEMY WHILE SPINNIN!");
        }


        timeUntilNextAttack = spinAttackDuration;
    }

    public void Accelerate()
    {
        Debug.Log("Zoom zoom");
        if(playerClock.localTimeScale <= 0.0f || globalClock.localTimeScale <= 0.0f)
        { // If rewinding, accelerate won't do anything
            return;
        }

        globalClock.localTimeScale = playerAcceleration;
        playerClock.localTimeScale = globalAcceleration;
    }

    public void Deaccelerate()
    {
        if (playerClock.localTimeScale <= 0.0f || globalClock.localTimeScale <= 0.0f)
        { // If rewinding, accelerate won't do anything
            return;
        }

        globalClock.localTimeScale = 1.0f;
        playerClock.localTimeScale = 1.0f;
    }

    public void CloneAttack()
    {
        // If there is a clone in the map, rewind to it
        GameObject cloneInstance = GameObject.FindGameObjectWithTag("Clone");

        if (cloneInstance != null)
        {
            Debug.Log("Rewind!");
            playerClock.localTimeScale = -1.0f;
            cloneInstance.GetComponent<CloneAttack>().playerRewinding = true;

            return;
        }
        else if (timeUntilNextAttack > 0.0f || _time.timeScale <= 0 || timeUntilNextCloneAttack > 0.0f)
        {
            return;
        }

        // Play attack animation
        //_animator.SetTrigger("SlashAttack");

        // Create the projectile that will act as the yoyo
        cloneInstance = Instantiate(clone, cloneAttackPoint.position, this.gameObject.transform.rotation);
        //GameObject projectile = Instantiate(extendAttackProjectile, extendAttackPoint.position, Quaternion.identity);


        CloneAttack cloneScript = cloneInstance.GetComponent<CloneAttack>();
        cloneScript.hitLayers = hitLayers;
        cloneScript.originPoint = cloneAttackPoint;
        cloneScript.damage = cloneAttackDamage;
        cloneScript.range = cloneAttackRange;
        cloneScript.duration = cloneAttackDuration;
        cloneScript._playerMovement = _playerMovement;

        cloneScript.RigToExplode();
        timeUntilNextCloneAttack = cloneAttackDuration;

        currentTime = 0.0f; // Reset time since clone spawned
    }


    private void OnDrawGizmosSelected()
    {
        //Gizmos.DrawWireSphere(slashAttackPoint.position, slashAttackRange);

        //Gizmos.DrawWireSphere(extendAttackPoint.position, extendAttackRange);
        //Gizmos.DrawWireSphere(extendAttackPoint.position + Vector3.right * extendAttackLength, extendAttackRange);

        //Gizmos.DrawCube(spinAttackPoint.position, spinAttackRange);
    }

    public void ToggleHitbox(int index, bool state)
    {
        //hitboxes[index].SetActive(state);
    }
}