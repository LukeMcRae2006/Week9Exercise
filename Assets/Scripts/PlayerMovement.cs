using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 move;
    [SerializeField] private Rigidbody2D rb;
    public float playerSpeed = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MyMove();
    }

    void MyMove()
    {
        rb.linearVelocity = move * playerSpeed * Time.deltaTime;
    }

    void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
        Debug.Log(move);
    }
}
