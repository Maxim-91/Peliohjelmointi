using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ReindeerLeader : MonoBehaviour
{
    public Transform[] points; // List of object locations where the poro will go: lentokentta_porot, lappiatalo, tottorakka
    private NavMeshAgent agent;
    private int currentPoint = 0;

    public float randR = 3f; // Radius for stopping at a random point

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // get NavMeshAgent for movement

        StartCoroutine(MovePoro());
    }

    IEnumerator MovePoro()
    {
        while (true)
        {
            // Super random point near the objective point
            Vector3 randomPoint = points[currentPoint].position + new Vector3(Random.Range(-randR, randR), 0, Random.Range(-randR, randR));

            // Send poro to this super random point
            agent.SetDestination(randomPoint);

            // Waiting until poro arrived
            while (agent.pathPending ||
                   agent.remainingDistance > agent.stoppingDistance ||
                   agent.velocity.magnitude > 0.1f)
            {
                yield return null;
            }

            // When arrive at the point, poro waits 5-20 sec. 
            float wait = Random.Range(5f, 20f);
            yield return new WaitForSeconds(wait);

            // then goes to the next point
            currentPoint = (currentPoint + 1) % points.Length;
        }
    }
}
