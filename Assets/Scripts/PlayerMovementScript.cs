using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    bool isGrounded = false;
    SpriteRenderer sr;
    Rigidbody2D rb;

    private void Start()
    {
        if (!rb)
            rb = GetComponent<Rigidbody2D>();
        if (!sr)
            sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        flipSprite();

        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
    }

    void flipSprite()
    {
        if (sr)
        {
            if (sr.flipX && speed > 0)
            {
                sr.flipX = false;
            }
            else if (!sr.flipX && speed < 0)
            {
                sr.flipX = true;
            }
        }
    }
    public void Movement(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        rb.velocity = new Vector2(input.x * speed, rb.velocity.y);
    }


    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
        else
            isGrounded = false;
    }
}