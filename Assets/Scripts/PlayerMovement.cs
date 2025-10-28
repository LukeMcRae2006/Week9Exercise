using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 move;

    [SerializeField] private Rigidbody2D rb;
    public float playerSpeed = 10f;

    [SerializeField] private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MyMove();
        FlipSprite();

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


}
