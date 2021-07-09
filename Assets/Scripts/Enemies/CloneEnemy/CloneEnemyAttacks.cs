using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneEnemyAttacks : MonoBehaviour
{
    // Animator
    private Animator _animator;

    public LayerMask hitLayers;

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

    // Start is called before the first frame update
    void Start()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // If not executing an attack, pick an attack after a bit (random time between interval), until then move OR avoid attack


    }

    private void PickAttack()
    {
        // If player far away but in line - Extend attack

        // If player not in line but far away and in front - Bomb attack
        
        // If player not in line  but far away and NOT in front - Clone attack (pick randonmly)

        // If player in front and close - Normal attack

        // Else just move
    }

    private void Move()
    {
        // Check if no attack is coming towards us, if it is, avoid it by jumping, else pick a target to walk to, and go towards it
    }

    public void SlashAttack()
    {
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
                if (doorControl.normalAttackUnlocks)
                    doorControl.UnlockDoor();
            }
        }
    }

    public void ExtendAttack()
    {
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

        projectileScript.SetSpeed();
    }

    public void SlowdownAttack()
    {
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

        projectileScript.slowdownForcefield = slowdownForcefield;
        projectileScript.forcefieldDuration = slowdownForcefieldDuration;


        projectileScript.SetParabolicVelocity();
        projectileScript.RigToExplode();
    }

}
