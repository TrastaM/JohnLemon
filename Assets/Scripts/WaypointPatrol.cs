using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    public float randomnessRadius = 2f; // Range for random offset

    int m_CurrentWaypointIndex;

    void Start()
    {
        SetRandomizedDestination(waypoints[0].position);
    }

    void Update()
    {
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            // Move to the next waypoint
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;

            // Set a new randomized destination
            SetRandomizedDestination(waypoints[m_CurrentWaypointIndex].position);
        }
    }

    void SetRandomizedDestination(Vector3 basePosition)
    {
        // Add a random offset within the defined radius value
        Vector3 randomOffset = new Vector3(
            Random.Range(-randomnessRadius, randomnessRadius),
            0f, // Keep the offset on the XZ plane
            Random.Range(-randomnessRadius, randomnessRadius)
        );

        Vector3 randomizedPosition = basePosition + randomOffset;

        // Ensure the randomized position is valid on the NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomizedPosition, out hit, randomnessRadius, NavMesh.AllAreas))
        {
            navMeshAgent.SetDestination(hit.position);
        }
        else
        {
            // Fallback to the base position if the random position is invalid
            navMeshAgent.SetDestination(basePosition);
        }
    }
}
