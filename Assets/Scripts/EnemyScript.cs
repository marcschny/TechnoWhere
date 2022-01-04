using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class EnemyScript : MonoBehaviour, IEnemy
{
    public bool selected;
    NavMeshAgent agent;


    private int randomSpot;

    private Animator anim;
    private float rotationSpeed = 300f;
    private bool playerSeen;
    public static bool playerCaught;
    private bool distracted;
    public float distractedTime = 10f;
    private Vector2 movementDirection;

    private GameObject player;
    private GameObject glowStick;


    public Transform[] points;
    private int destPoint = 0;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        playerCaught = false;


        player = GameObject.FindWithTag("Player");


        anim = GetComponent<Animator>();
        GotoNextPoint();
    }

    void Update()
    {
        if (selected)
        {
            // To debug individual enemy
        }

        //Debug.DrawLine(transform.position, player.transform.position, Color.red);

        movementDirection = agent.steeringTarget - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movementDirection);

        //rotate enemy & start srpite animation if enemy is moving
        if (movementDirection != Vector2.zero)
        {
            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            anim.enabled = true;
        }
        else
        {
            anim.enabled = false;
        }

        if (!agent.pathPending && !playerSeen && !distracted && agent.remainingDistance < 0.5f)
            GotoNextPoint();




        if (playerSeen && !distracted)
        {
            agent.SetDestination(player.transform.position);

            if (agent.remainingDistance < 1f)
            {
                Caught();
            }

            if (agent.remainingDistance > 17.5f &&
                agent.remainingDistance <
                1000f) // sometimes it returns infinity during the calculation, therefore an upper limit
            {
                Debug.Log("too far away");
                Debug.Log(agent.remainingDistance);
                playerSeen = false;
                agent.speed = 3.5f;
                GotoNextPoint();
            }
        }
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }


    private IEnumerator Distraction()
    {
        distracted = true;
        agent.SetDestination(glowStick.transform.position);
        yield return new WaitForSeconds(distractedTime);
        Destroy(glowStick);

        distracted = false;
        GotoNextPoint();
    }


    public void onTrigger()
    {
        NavMeshHit hit;
        if (!agent.Raycast(player.transform.position, out hit))
        {
            Debug.Log(hit);
            // Target is "visible" from our position.
            playerSeen = true;
            agent.speed = 6f;

            Debug.Log("Player has been seen!");
        }
    }

    public void onTriggerGlowStick(GameObject glowStick)
    {
        NavMeshHit hit;

        this.glowStick = glowStick;

        if (!agent.Raycast(this.glowStick.transform.position, out hit))
        {
            Debug.Log(hit);
            // Target is "visible" from our position.
            StartCoroutine(Distraction());

            Debug.Log("Glowstick has been seen!");
        }
    }


    public void Caught()
    {
        playerCaught = true;
        //Debug.Log("Player Caught");
    }
}