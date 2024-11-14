using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    // Variables
    public float speed = 5f;         // Movement speed
    public float jumpForce = 5f;     // Jump force
    private bool isGrounded = true;  // Checks if the player is on the ground

    private Rigidbody2D rb;
    private Animator animator;// Reference to Animator
    private SpriteRenderer sprite;

    void Start()
    {
        // Initialize Rigidbody2D and Animator
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Get player input
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Horizontal movement
        Vector2 movement = new Vector2(moveHorizontal * speed, rb.linearVelocity.y);
        rb.linearVelocity = movement;

        // Update Animator isWalking parameter based on movement
        if (moveHorizontal != 0) // If player is moving horizontally
        {
            if (moveHorizontal > 0)
            {
                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }
            animator.SetBool("isWalking", true);

        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }
}