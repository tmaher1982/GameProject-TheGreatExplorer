using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{

    //This is straight from the unity manual
    public Transform[] points;
    public bool randomWaypointOrder = true;
    private int destPoint = 0;
    private NavMeshAgent agent;
    Animator animator;

    // Creating your first animated AI Character! [AI #01]
    // https://www.youtube.com/watch?v=TpQbqRNCgM0
    void Start()
    {
         agent = GetComponent<NavMeshAgent>();
         animator = GetComponent<Animator>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        if(randomWaypointOrder)
        {
            // Choose random waypoint
            destPoint = Random.Range(0, points.Length);
        }
        else
        {
            destPoint = 0;
        }

        GotoNextPoint();
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
        // destPoint = (destPoint + 1) % points.Length;
        
        if(randomWaypointOrder)
        {
            // Choose random waypoint
            destPoint = Random.Range(0, points.Length);
        }
        else
        {
            destPoint = (destPoint + 1) % points.Length;
        }
    }
    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);
        
        // Choose the next destination point when the agent gets
        // close to the current one.
        
        if (!agent.pathPending && agent.remainingDistance < 0.25f)
            GotoNextPoint();
    }
}
