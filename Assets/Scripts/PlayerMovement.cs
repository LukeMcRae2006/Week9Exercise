using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 move;

    [SerializeField] private Rigidbody2D rb;
    public float playerSpeed = 10f;
    public float jumpForce = 10f;
    public float climbSpeed = 5f;

    [SerializeField] private BoxCollider2D box;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask ladderLayer;

    [SerializeField] private Animator anim;
    [SerializeField] private float coyoteTime = 1f;
    private float lastTimeSinceGrounded = 1;
    private bool canJump = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MyMove();
        FlipSprite();
        ClimbLadder();

        if (box.IsTouchingLayers(groundLayer))
        {
            //we are grounded
            canJump = true;
            coyoteTime = 1f;


        }
        else if (canJump != false)
        {
            //we are not grounded therefore we must countdown coyotetime
            if (coyoteTime > 0)
            {
                coyoteTime -= Time.deltaTime;


            }
            else
            {
                canJump = false;
            }
        }

    }


    void MyMove()
    {
        rb.linearVelocity = new Vector2(move.x * playerSpeed, rb.linearVelocity.y);

    }

    void FlipSprite()
    {
        bool hasHorizontalSpeed = Math.Abs(rb.linearVelocity.x) > Mathf.Epsilon;
        anim.SetBool("IsMoving", hasHorizontalSpeed);
        if (hasHorizontalSpeed)
        {


            transform.localScale = new Vector2(Mathf.Sign(rb.linearVelocity.x), 1f);
        }
    }

    void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
        Debug.Log(move);
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed && canJump)
        {
            rb.linearVelocity += new Vector2(0f, jumpForce);
            canJump = false;
        }
    }

    void ClimbLadder()
    {
        if (box.IsTouchingLayers(ladderLayer))
        {
            anim.SetBool("IsClimbing", true);
            rb.gravityScale = 0;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, move.y * climbSpeed);
        }
        else
        {
            anim.SetBool("IsClimbing", false);
            rb.gravityScale = 1;
        }

    }


}
