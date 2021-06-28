using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public int jumpForce = 700;
    public float runSpeed = 40;
    public float movementSmoothing = 0.05f;
    public float groundedRadius = .2f;
    public float ceilingRadius = .2f;
    public Collider2D crouchCollider;

    public LayerMask whatIsGround;
    public Transform groundCheck;
    public Transform ceilingCheck;

    private bool _facingRight = true;
    private bool _grounded = true;
    private bool _crouching = false;

    //private Animator _animator;
    private Rigidbody2D _body;

    private Vector2 _velocity;

    [Header("Events")] [Space] public UnityEvent OnLandEvent;
    public BoolEvent OnCrouchEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool>
    {
    }


    private void Start()
    {
        //_animator = GetComponent<Animator>();
        _body = GetComponent<Rigidbody2D>();
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

    public void Move(float move, bool jump, bool crouch)
    {
        move *= runSpeed * Time.deltaTime;


        if (!crouch && _grounded)
        {
            if (Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatIsGround))
            {
                crouch = true;
            }
        }

        if (crouch && _grounded)
        {
            if (!_crouching)
            {
                _crouching = true;
                _body.velocity = Vector2.zero;
                OnCrouchEvent.Invoke(true);
            }

            // Disable one of the colliders when crouching
            if (crouchCollider != null)
                crouchCollider.enabled = false;
        }
        else
        {
            // Enable the collider when not crouching
            if (crouchCollider != null)
                crouchCollider.enabled = true;

            if (_crouching)
            {
                _crouching = false;
                OnCrouchEvent.Invoke(false);
            }
        }

        if (!crouch)
        {
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

        // If the player should jump...
        if (_grounded && jump)
        {
            // Add a vertical force to the player.
            _grounded = false;
            _body.AddForce(new Vector2(0f, jumpForce));
        }
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