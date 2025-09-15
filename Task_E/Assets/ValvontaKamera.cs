using System;
using UnityEngine;

public class ValvontaKamera : MonoBehaviour
{
    public float speed = 0.05f; // Camera rotation speed    
    private float angleRange;
    private Vector3 leftDir;
    private Vector3 rightDir;    
    private float t = 0;
    private bool follow = false;
    private Vector3 newDir;
    private Vector3 targetDir;

    public Transform player; // Player object
    public float detectionRange = 20f; // player detection range
    public float playerVisibleTime = 0f; // it for calculating the time the player is visible to the camera

    void Start()
    {
        angleRange = UnityEngine.Random.Range(120f, 180f); // Kameran kääntyilee 120-180° (randomilla)

        leftDir = Quaternion.Euler(0, -angleRange / 2, 0) * Vector3.forward; // Left direction
        rightDir = Quaternion.Euler(0, angleRange / 2, 0) * Vector3.forward; // // Right direction               
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < detectionRange) // Follow the player because he is close
        {
            follow = true;
            t = 0.02f; // Speed of camera rotation to the player

            /* The direction from the camera to the player (targetDir) is calculated, 
            then the camera is smoothly rotated to this direction using Vector3.Lerp, 
            and the new direction is applied using Quaternion.LookRotation. */
            targetDir = (player.position - transform.position).normalized;
            newDir = Vector3.Lerp(transform.forward, targetDir, t);
            transform.rotation = Quaternion.LookRotation(newDir);

            playerVisibleTime += Time.deltaTime; // Add player's time in the field of view
            
        }
        else
        {
            t = Mathf.PingPong(Time.time * speed, 1f); // Сyclic fluctuation of a value between Time.time * speed and 1
            Vector3 patrolDir = Vector3.Lerp(leftDir, rightDir, t).normalized;

            if (follow) // I return the camera from following the player to the rotate direction
            {
                newDir = Vector3.Lerp(transform.forward, patrolDir, speed * 0.2f).normalized;
                transform.rotation = Quaternion.LookRotation(newDir);

                if (Vector3.Angle(transform.forward, patrolDir) < 0.5f)
                {
                    follow = false;
                    Console.WriteLine("Player visible time: " + playerVisibleTime);
                    playerVisibleTime = 0f; // Reset the visible time counter
                }
            }
            else // Rotate camera 120-180° (random)
            {
                newDir = Vector3.Lerp(leftDir, rightDir, t).normalized;
                transform.rotation = Quaternion.LookRotation(newDir);
            }
        }
    }
}