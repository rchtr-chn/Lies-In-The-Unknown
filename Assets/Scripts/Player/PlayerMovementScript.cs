using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;

    float intervalTimer = 0f;
    float sfxInterval = 0.5f; // Interval for walking sound effects

    bool isGrounded = false;
    SpriteRenderer sr;
    Rigidbody2D rb;
    public Animator animator;
    public AudioManagerScript audioManager;

    private void Start()
    {
        if (audioManager == null && GameObject.Find("AudioManager") != null)
        {
            audioManager = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();
        }
        if (!animator)
        {
            animator = GetComponent<Animator>();
        }
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

        WalkSFX();

        animator.SetFloat("velocity", rb.velocity.magnitude);
    }

    void flipSprite()
    {
        if (sr)
        {
            if (rb.velocity.x < 0)
            {
                sr.flipX = false; // Facing right
            }
            else if (rb.velocity.x > 0)
            {
                sr.flipX = true; // Facing left
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

            audioManager.PlaySfx(audioManager.playerJumpSfx);

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

    void WalkSFX()
    {
        if (rb.velocity.magnitude > 0.1f && isGrounded)
        {
            intervalTimer += Time.deltaTime;
            if (intervalTimer > sfxInterval)
            {
                intervalTimer = 0f;
                audioManager.PlaySfx(audioManager.playerRunSfx);
            }
        }
        else
        {
            intervalTimer = 0f;
        }
    }
}