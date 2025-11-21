using UnityEngine;
using UnityEngine.AI;

public class FollowReindeer : MonoBehaviour
{
    public Transform leader; // The poro leader location 

    // Distance to follow, for example, for first 0.5, but for second 0.7
    public float followDistance = 0.5f; 
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    { 
        // Point behind the leader
        Vector3 targetPos = leader.position - leader.forward * followDistance;

        agent.SetDestination(targetPos);
    }
}
