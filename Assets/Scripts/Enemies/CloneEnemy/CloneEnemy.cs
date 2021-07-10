using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneEnemy : EnemyBase
{
    // Animator
    private Animator _animator;

    public LayerMask hitLayers;

    private bool _grounded;
    private float timeUntilNextAttack = 0.0f;


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
    private bool _spinAttacking = false;

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
        if (timeUntilNextAttack > 0.0f)
        {
            timeUntilNextAttack = Mathf.Max(0.0f, timeUntilNextAttack - Time.deltaTime);
        }
        else
        {
            PickAttack();
        }

        if (_spinAttacking)
        { // Check spin attack collisions if we're spin attacking
          // Detect enemies and doors in range of attack
            Collider2D[] hits = Physics2D.OverlapBoxAll(spinAttackPoint.position, spinAttackRange, 0.0f, hitLayers);

            // Damage enemies and unlock doors
            foreach (Collider2D hit in hits)
            {
                if (hit.gameObject.layer == 6) // Hit player
                    hit.GetComponent<PlayerHealth>().Hit(slashAttackDamage);
            }
        }

    }

    private void PickAttack()
    {
        // If player far away but in line - Extend attack

        // If player not in line but far away and in front - Bomb attack

        // If player not in line  but far away and NOT in front - Clone attack (pick randonmly)

        // If player in front and close - Normal attack
        SlashAttack();
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
            if (hit.gameObject.layer == 6)
            { // Hit player
                hit.transform.parent.parent.GetComponent<PlayerHealth>().Hit(slashAttackDamage);
                break;
            }
        }

        timeUntilNextAttack = Random.Range(slashAttackDuration, 4.0f);
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

        timeUntilNextAttack = Random.Range(extendAttackDuration, 4.0f);
    }

    public void slowdownAttack()
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

        timeUntilNextAttack = Random.Range(slowdownAttackDuration, 4.0f);
    }


    public void SpinAttack()
    {
        //Jump();
        _animator.SetTrigger("SpinAttack");
        _spinAttacking = true;

        StartCoroutine(SpinAttacking());


        timeUntilNextAttack = Random.Range(spinAttackDuration, 4.0f);
    }

    IEnumerator SpinAttacking()
    {

        yield return new WaitForSeconds(spinAttackDuration);
        _spinAttacking = false;
    }

    public void CloneAttack()
    {
        // Create the projectile that will act as the yoyo
        GameObject cloneInstance = Instantiate(clone, cloneAttackPoint.position, this.gameObject.transform.rotation);
        //GameObject projectile = Instantiate(extendAttackProjectile, extendAttackPoint.position, Quaternion.identity);


        CloneAttack cloneScript = cloneInstance.GetComponent<CloneAttack>();
        cloneScript.hitLayers = hitLayers;
        cloneScript.originPoint = cloneAttackPoint;
        cloneScript.damage = cloneAttackDamage;
        cloneScript.range = cloneAttackRange;
        cloneScript.duration = cloneAttackDuration;

        cloneScript.RigToExplode();
        timeUntilNextAttack = Random.Range(2.0f, 4.0f);
    }


}
