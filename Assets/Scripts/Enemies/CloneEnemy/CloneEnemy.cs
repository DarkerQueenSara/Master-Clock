using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CloneEnemy : EnemyBase
{
    private GameObject player;

    // Animator
    private Animator _animator;

    public LayerMask hitLayers;

    private float timeUntilNextAttack = 0.0f;

    public float minDistForLongRangeAttacks = 3.0f;

    private Vector3 targetPosition;

    /* Movement */
    [Header("Movement")]
    public int jumpForce = 10000;
    public float runSpeed = 40;
    public float movementSmoothing = 0.05f;
    public float groundedRadius = .2f;
    public LayerMask whatIsGround;
    public Transform groundCheck;

    private float timeUntilNextMoveTarget = 0.0f;
    public float minTimeBetweenNewMoveTargets = 3.0f;
    public float maxTimeBetweenNewMoveTargets = 5.0f;

    [Header("Events")] [Space] public UnityEvent OnLandEvent;

    private Vector2 _velocity;
    private Rigidbody2D _body;
    private bool _grounded;
    private bool _facingRight = true;


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
        _body = gameObject.GetComponent<Rigidbody2D>();

        player = PlayerEntity.instance.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // If not executing an attack, pick an attack after a bit (random time between interval), until then move OR avoid attack
        /* ATTACK */
        if (timeUntilNextAttack > 0.0f)
        {
            timeUntilNextAttack = Mathf.Max(0.0f, timeUntilNextAttack - Time.deltaTime);
        }
        else
        {
            PickAttack();
        }
        

        /* MOVEMENT */
        if(timeUntilNextMoveTarget > 0.0f)
        {
            timeUntilNextMoveTarget = Mathf.Max(0.0f, timeUntilNextMoveTarget - Time.deltaTime);
        }
        else
        {
            PickNewTargetPosition();
            timeUntilNextMoveTarget = Random.Range(minTimeBetweenNewMoveTargets, maxTimeBetweenNewMoveTargets);
        }

        if (Vector3.Distance(transform.position, targetPosition) >= 0.5f)
        {
            Move();
        }
        else
        {
            StopMoving();
        }


        if (_spinAttacking)
        { // Check spin attack collisions if we're spin attacking
          // Detect enemies and doors in range of attack
            Collider2D[] hits = Physics2D.OverlapBoxAll(spinAttackPoint.position, spinAttackRange, 0.0f, hitLayers);

            // Damage enemies and unlock doors
            foreach (Collider2D hit in hits)
            {
                if (hit.gameObject.layer == 6)
                { // Hit player
                    hit.transform.parent.parent.GetComponent<PlayerHealth>().Hit(spinAttackDamage);
                    break;
                }
            }
        }

    }

    private void FixedUpdate()
    {
        bool wasGrounded = _grounded;
        _grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                _grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }

    private void PickAttack()
    {
        Vector3 playerPosition = player.transform.position;
        float distance = Vector3.Distance(this.transform.position, playerPosition);

        Debug.Log(distance);
        // Long Range Attacks //
        if (distance > minDistForLongRangeAttacks)
        {
            // If player far away but in line - Extend attack
            if (Mathf.Abs(playerPosition.y - transform.position.y) < 1.0f)
            {
                FacePlayer();
                ExtendAttack();
            }

            // If player not in line but far away and in front - Bomb attack
            else if (CheckPlayerInFront())
            {
                SlowdownAttack();
            }
            // If player not in line  but far away and NOT in front - Clone attack
            else
            {
                CloneAttack();
            }
        }
        // Short Range Attacks //
        else
        {
            // If player in front and close - Normal attack
            if (CheckPlayerInFront())
                SlashAttack();
            else // Else, spin attack
                SpinAttack();
        }
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
        //animator.SetTrigger("SlashAttack");

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
        projectileScript._facingRight = _facingRight;


        projectileScript.SetSpeed();

        timeUntilNextAttack = Random.Range(extendAttackDuration, 4.0f);
    }

    public void SlowdownAttack()
    {
        // Play attack animation
        //animator.SetTrigger("SlashAttack");

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
        projectileScript._facingRight = _facingRight;

        projectileScript.slowdownForcefield = slowdownForcefield;
        projectileScript.forcefieldDuration = slowdownForcefieldDuration;


        projectileScript.SetParabolicVelocity();
        projectileScript.RigToExplode();

        timeUntilNextAttack = Random.Range(slowdownAttackDuration, 4.0f);
    }


    public void SpinAttack()
    {
        Jump();
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


    /* MOVEMENT */
    public void Jump()
    {
        // Add a vertical force to the player.
        _body.velocity = new Vector2(_body.velocity.x, 0.0f);
        _grounded = false;
        _body.AddForce(new Vector2(0f, jumpForce));
    }

    public void FacePlayer()
    {
        if (!CheckPlayerInFront())
        {
            Flip();
        }
    }

    public void Flip()
    {
        // Switch the way the player is labelled as facing.
        _facingRight = !_facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }

    public bool CheckPlayerInFront()
    {
        if (_facingRight)
        {
            return player.transform.position.x >= transform.position.x;
        }
        else
        {
            return player.transform.position.x <= transform.position.x;
        }
    }

    public bool CheckTargetPosToRight()
    {
        return targetPosition.x >= transform.position.x;
    }

    private void PickNewTargetPosition()
    {
        float distanceToTravel = Random.Range(3.0f, 5.0f);

        if (Random.Range(0.0f, 1.0f) < 0.5f)
        {
            targetPosition = transform.position + transform.right * distanceToTravel;
        }
        else
        {
            targetPosition = transform.position - transform.right * distanceToTravel;
        }
    }

    private void Move()
    {
        float move = runSpeed * Time.deltaTime;
        if (!CheckTargetPosToRight())
        {
            move *= -1.0f;
        }

        // Set animation to move
        _animator.SetFloat("Speed", Mathf.Abs(move));

        // Move the character by finding the target _velocity
        Vector3 targetVelocity = new Vector2(move * 10f, _body.velocity.y);
        // And then smoothing it out and applying it to the character
        _body.velocity = Vector2.SmoothDamp(_body.velocity, targetVelocity, ref _velocity, movementSmoothing);

        // If the input is moving the player right and the player is facing left...
        if (move > 0 && !_facingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (move < 0 && _facingRight)
        {
            // ... flip the player.
            Flip();
        }


        if (!_grounded && !_spinAttacking)
        {
            _animator.SetFloat("Verticle_Speed", _body.velocity.y);
        }
        else
        {
            _animator.SetFloat("Verticle_Speed", 0.0f);
        }
    }

    public void StopMoving()
    {
        _body.velocity = Vector3.zero;
        _animator.SetFloat("Speed", 0.0f);
    }
}
