/*
Lucas Ayres Student ID 2321346 
All code was created using the following sources:
 Pandemonium(4/12/2020) Unity 2D Platformer for Complete Beginners Playlist Accessed: 20/10/2024
 Game Maker's Toolkit(2/12/2022) The Unity Tutorial For Complete Beginners Accessed: 20/10/2024
 Brackey's(22/1/2017) How to make a Video Game - Getting Started (Unity) Accessed: 20/10/2024
 Stack Overflow Community. (2015). Spawning player at certain point in Unity [Online]. Stack Overflow. 
 Available from: https://stackoverflow.com/questions/31565355/spawning-player-at-certain-point-in-unity?rq=3 [Accessed 27 Oct 2024]
 Unity Documentation was also used but I didn't get anything the series did not cover. 
 ChatGTP was also used to grade Code for maintability and usability - no content generated only gave suggestions
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float baseSpeed = 5f;//Player's staring speed'
    public float jumpForce = 5f;//Player's jump height/force'
    public float speedIncreasePerScore = 0.1f;//As player gains score, this increases the player's speed at this rate'
    [SerializeField]private float currentSpeed; //Used to set the new speed for the player
   


    private Rigidbody2D RigidbodyP;
    private bool isGrounded;

    void Start()
    {
        RigidbodyP = GetComponent<Rigidbody2D>();
        currentSpeed = baseSpeed; //Set currentSpeed at baseSpeed 
    }

    void Update()
    {
        //Allows for currentSpeed to be set as player gains score
        UpdateSpeed();

        //Allows player to move left/right
        float move = Input.GetAxis("Horizontal");
        RigidbodyP.velocity = new Vector2(move * currentSpeed, RigidbodyP.velocity.y);

        //Allows player to jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            RigidbodyP.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }
        //Checks if player is grounded 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    //Checks if player is grounded 
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