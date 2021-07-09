using System;
using UnityEngine;
using UnityEngine.Events;
using Chronos;

public class PlayerMovement : MonoBehaviour
{
    public int jumpForce = 700;
    public float runSpeed = 40;
    public float movementSmoothing = 0.05f;
    public float groundedRadius = .2f;
    public float ceilingRadius = .2f;

    public Collider2D standCollider;
    public Collider2D slideCollider;
    public Collider2D jumpCollider;

    public LayerMask whatIsGround;
    public Transform groundCheck;
    public Transform ceilingCheck;

    [HideInInspector] public bool _facingRight = true;

    private bool _grounded = true;
    private bool _sliding = false;

    private Animator _animator;

    //private Rigidbody2D _body;
    private RigidbodyTimeline2D _body;

    private Vector2 _velocity;

    // Time Stuff
    private Timeline _time;

    public bool moveBlocked = false;

    [Header("Events")] [Space] public UnityEvent OnLandEvent;
    public BoolEvent OnSlideEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool>
    {
    }


    private void Start()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();
        //_body = GetComponent<Rigidbody2D>();
        _time = GetComponent<Timeline>();
        _body = _time.rigidbody2D;
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

    public void Move(float move, bool jump, bool slide)
    {
        _animator.SetBool("MovementBlocked", moveBlocked);

        if (moveBlocked)
        {
            return;
        }

        /* SLIDE OLD */
        /*
        if (slide && _body.velocity != Vector2.zero) // If clicked to slide and we have some speed (we can't slide if we're standing still)
        {
            if (_grounded)
            {
                _sliding = true;

                // Swap Colliders
                slideCollider.enabled = true;

                standCollider.enabled = false;
            }
        }
        else if (!Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatIsGround)) // If we stopped wanting to slide and we can stand up
        {
            _sliding = false;

            // Swap Colliders
            slideCollider.enabled = false;

            standCollider.enabled = true;
        }
        */

        /* SLIDE NEW */
        if (slide && jump && _grounded && _time.timeScale > 0)
        {
            // Add velocity in the right direction
            move = _facingRight ? 1 : -1;
            move *= runSpeed * Time.deltaTime;
            Vector3 targetVelocity = new Vector2(move * 10f, _body.velocity.y);
            _body.velocity = Vector2.SmoothDamp(_body.velocity, targetVelocity, ref _velocity, movementSmoothing);

            _sliding = true;

            // Swap Colliders
            slideCollider.enabled = true;

            standCollider.enabled = false;
        }
        else if (!slide && _sliding && !Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatIsGround))
        {
            _sliding = false;

            // Swap Colliders
            slideCollider.enabled = false;

            standCollider.enabled = true;
        }

        Debug.Log(_sliding);
        _animator.SetBool("Sliding", _sliding);


        /* MOVE */
        move *= runSpeed * Time.deltaTime;

        if (!_sliding && _time.timeScale > 0) // Move only when time is going forward and not sliding
        {
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
        }

        /* JUMP */

        // If the player should jump...
        if (_grounded && jump)
        {
            if (!_sliding)
            {
                // Add a vertical force to the player.
                _body.velocity = new Vector2(_body.velocity.x, 0.0f);
                _grounded = false;
                _body.AddForce(new Vector2(0f, jumpForce));
            }
        }

        if (!_grounded && !_sliding && !moveBlocked)
        {
            _animator.SetFloat("Verticle_Speed", _body.velocity.y);
        }
        else
        {
            _animator.SetFloat("Verticle_Speed", 0.0f);
        }
    }

    public void StopPlayer()
    {
        _body.velocity = Vector3.zero;
        _animator.SetFloat("Speed", 0.0f);
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        _facingRight = !_facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}