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

public class EnemyMelee : MonoBehaviour
{
    //Once player gets too close they are instantly respawned 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Find the Respawn script on the player
            Respawn respawn = other.GetComponent<Respawn>();
            if (respawn != null)
            {
                Debug.Log("Player caught by enemy! Respawning...");
                respawn.DieAndRespawn(); // Respawn the player

      
            }
        }
    }
}