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

//Setting Timer up for fail condition

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public float countdownTime = 60f; //Set time for Timer 
    private float currentTime; //Create variable for getting the current time on timer

    private bool isGameOver = false;

    //At Game Start, begin timer
    void Start()
    {
        currentTime = countdownTime;
    }

    //Every Frame, count down timer and see if it hits 0
    void Update()
    {
        if (!isGameOver)
        {
            // Count down the timer
            currentTime -= Time.deltaTime;

            // Check if time has run out
            if (currentTime <= 0)
            {
                //Ends Game when Timer is 0
                GameOver();
            }
        }
    }

    void GameOver()
    {
        isGameOver = true; //Check timer is Zero (As that sets GameOver)
        Debug.Log("Time's up! Player is dead.");
        KillPlayer();
    }

    void KillPlayer()
    {
        //Find Player 
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Destroy(player); //Destroy Player Character
    }

    public float GetTimeRemaining()
    {
        return Mathf.Max(0, currentTime); //Timer can only fall to zero not negative
    }
}