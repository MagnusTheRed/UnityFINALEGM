using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float baseSpeed = 5f; // Player's starting speed
    public float jumpForce = 5f; // Player's jump height/force
    public float speedIncreasePerScore = 0.1f; // Rate at which player's speed increases with score
    [SerializeField] private float currentSpeed; // Used to set the new speed for the player

    private Rigidbody2D RigidbodyP;
    private bool isGrounded;

    // Animator reference
    //private Animator animator; - this didn't work so removed 

    void Start()
    {
        RigidbodyP = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Get Animator component attached to the player
        currentSpeed = baseSpeed; // Set currentSpeed to baseSpeed 
    }

    void Update()
    {
        // Allows for currentSpeed to be set as player gains score
        UpdateSpeed();

        // Get horizontal movement input
        float move = Input.GetAxis("Horizontal");

        // Update player velocity
        RigidbodyP.velocity = new Vector2(move * currentSpeed, RigidbodyP.velocity.y);

        // Update animation parameters
        animator.SetFloat("Speed", Mathf.Abs(move)); // Set Speed parameter to absolute movement value

        // Flip player sprite based on movement direction
        if (move > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Facing right
        }
        else if (move < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Facing left
        }

        // Allows player to jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            RigidbodyP.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            //animator.SetTrigger("Jump"); // Trigger the Jump animation - this didn't work 
        }

        // Update grounded animation state
        //animator.SetBool("IsGrounded", isGrounded); - this didn't work so listed as a bug
    }

    // Checks if player is grounded
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // Checks if player leaves the ground
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void UpdateSpeed()
    {
        // Get the score from ScoreManager
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager != null)
        {
            currentSpeed = baseSpeed + (scoreManager.score * speedIncreasePerScore);
        }
    }
}
