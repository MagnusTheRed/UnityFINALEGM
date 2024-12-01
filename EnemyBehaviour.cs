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

public class EnemyBehaviour : MonoBehaviour
{
    public int EnemyHealth = 50;          //Health of the enemy
    public float patrolSpeed = 2f;       //Patrol Speed
    public float chaseSpeed = 4f;        //Chase Speed yer
    public float attackRange = 1.5f;    //Attack Range - can be used for melee but now redudant due to collision kill 
    public float detectionRange = 5f;   //Aggro Range
    public float attackCooldown = 2f;   //redudant
    public int attackDamage = 10;       //redudant

    private Transform player;           //Refers to the player character
    private PlayerStealthState playerStealthState; //Refers to player hidden or not hidden
    private Vector2 initialPosition;    //Where the enemy starts
    private bool isChasing = false;     //If the enemy is chasing the Player
    private bool isAttacking = false;   // Whether the enemy is attacking
    private float attackTimer = 0f;     // Cooldown timer for attacks

    public Transform pointA;            //First thing/area to head towards
    public Transform pointB;            //Second thing/area to head towards 
    private Transform currentPatrolPoint;

    private Rigidbody2D rb;

    public AudioClip alertSound;        //Can be used to set a detection alert sound
    private AudioSource audioSource;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerStealthState = player.GetComponent<PlayerStealthState>();
        initialPosition = transform.position;
        currentPatrolPoint = pointA;
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        //Check if the player is 1.detected and 2,not hidden
        if (distanceToPlayer <= detectionRange && (playerStealthState == null || !playerStealthState.IsHidden()))
        {
            if (!isChasing) PlayAlertSound(); //Play alert sound when detecting the player
            isChasing = true;
        }
        else if (distanceToPlayer > detectionRange * 1.5f)
        {
            isChasing = false;
        }

        // Chase or patrol
        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }

        // Check if the player is within attack range and attack is off cooldown --redudant
        if (distanceToPlayer <= attackRange && attackTimer <= 0f)
        {
            StartCoroutine(Attack());
        }

        attackTimer -= Time.deltaTime;
    }

    void Patrol()
    {
        if (isAttacking || isChasing) return; //Stop patrol if chasing or attacking

        Vector2 targetPosition = currentPatrolPoint.position;
        Vector2 newPosition = Vector2.MoveTowards(transform.position, targetPosition, patrolSpeed * Time.deltaTime);
        rb.MovePosition(newPosition);

        //Switch patrol points if close to the current one
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentPatrolPoint = currentPatrolPoint == pointA ? pointB : pointA;
        }
    }

    void ChasePlayer()
    {
        if (isAttacking) return;

        Vector2 playerPosition = player.position;
        Vector2 newPosition = Vector2.MoveTowards(transform.position, playerPosition, chaseSpeed * Time.deltaTime);
        rb.MovePosition(newPosition);
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        attackTimer = attackCooldown;


        yield return new WaitForSeconds(0.5f); // Attack wind-up time

        //Check if the player is still in range
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange)
        {
            //Deal damage to the player
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }

        yield return new WaitForSeconds(0.5f); 
        isAttacking = false;
    }

    public void TakeDamage(int damage)
    {
        EnemyHealth -= damage;
        Debug.Log($"Enemy took {damage} damage. Remaining health: {EnemyHealth}");

        if (EnemyHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy has died!");
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        //Show detection and attack ranges in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void PlayAlertSound()
    {
        if (audioSource != null && alertSound != null)
        {
            audioSource.PlayOneShot(alertSound);
        }
    }
}