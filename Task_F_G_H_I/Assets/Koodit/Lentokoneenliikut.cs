/* To describe the airpalne's movement used AI: Google Gemini and from Visual Studio Code Copilot.

I divided the plane movement into the following stages:
1. Acceleration on the runway.
2. At the end of the runway the plane continues to accelerate and start fly up.
3. At "heightLevel" position, the plane makes a circular motion, covering 420 degrees.
4. Having flown a 420 degree circle, the plane heads towards the runway, slowing down.
5. On the runway, the plane continues to slow down and stop.
6. After a pause, the plane returns to its starting position and rotation. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Lentokoneenliikut : MonoBehaviour
{
    private GameObject lentokone;
    private Vector3 startPoint;
    private Quaternion startDirection;

    private float currentSpeed = 0f;
    public float acceleration = 15f; 
    private Vector3 currentDirection; // Direction of airplane movement
    public Vector3 runwayEndPoint = new Vector3(45.17f, -71.2334f, -142.3f); // The end of the runway, need go up after
    public float heightLevel = -50f; // The altitude to which an airplane takes off
    public float circleRadius = 50f; // Radius of the circle flight
    public float speedRotate = 5f; // Turning speed
    public float pauseTime = 3f; // A pause before the start of a new flight

    void Awake()
    {
        lentokone = GameObject.Find("lenskari"); // Find the airplane object        
    }

    void Start()
    {        
        startPoint = lentokone.transform.position; // Store the starting position
        startDirection = lentokone.transform.rotation; // Store the starting rotation

        StartCoroutine(Fly());        
    }
    
    void Update()
    {
        
    }

    IEnumerator Fly()
    {
        while (true) // Infinite loop
        {
            //-------------------------------------------------------------------------------------------------------------------------------
            Debug.Log("Ground Run: from startPoint to runwayEndPoint.");

            currentSpeed = 0f;
            
            while (Vector3.Distance(lentokone.transform.position, runwayEndPoint) > 0.5f) // Work Distance from startPoint to runwayEndPoint
            {
                currentSpeed += acceleration * Time.deltaTime; // Acceleration

                // Direction from current position to runwayEndPoint
                currentDirection = (runwayEndPoint - lentokone.transform.position).normalized;
                
                // Move the object in currentDirection with the current speed
                lentokone.transform.position += currentDirection * currentSpeed * Time.deltaTime;
                
                yield return null;
            }

            //-------------------------------------------------------------------------------------------------------------------------------
            Debug.Log("Start fly: from runwayEndPoint to heightLevel.");
            while (lentokone.transform.position.y < heightLevel) // Work Distance from runwayEndPoint to heightLevel
            {
                currentSpeed += acceleration * Time.deltaTime; // Acceleration

                // Move the plane upwards
                Vector3 moveUp = currentDirection * currentSpeed * Time.deltaTime + Vector3.up * currentSpeed * Time.deltaTime;            
                lentokone.transform.position += moveUp;           

                yield return null;
            }     

            //-------------------------------------------------------------------------------------------------------------------------------
            Debug.Log("Flying around the city in a circle.");

            float angle = 0f;
            Vector3 circleStartPoint = lentokone.transform.position; // The starting point and also center of the circle

            while (angle < 420f) // // Work Distance from heightLevel to circle 420 degrees
            {
                // Calculate the new position around the circle
                float rad = angle * Mathf.Deg2Rad; // Convert degrees to radians
                float x = circleStartPoint.x + circleRadius * Mathf.Cos(rad); // X coordinate on the circle
                float z = circleStartPoint.z + circleRadius * Mathf.Sin(rad); // Z coordinate on the circle
                Vector3 newPosition = new Vector3(x, lentokone.transform.position.y, z); // New position on the circle at the same height            

                // Move the plane to newPosition with current speed
                lentokone.transform.position = Vector3.MoveTowards(lentokone.transform.position, newPosition, currentSpeed * Time.deltaTime);

                angle += currentSpeed / circleRadius * Time.deltaTime * Mathf.Rad2Deg; // Angle increase based on speed and radius

                // Smoothly turning the nose of the plane toward the newPosition
                Vector3 direction = (newPosition - lentokone.transform.position).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                lentokone.transform.rotation = Quaternion.Slerp(lentokone.transform.rotation, targetRotation, speedRotate * Time.deltaTime);

                yield return null;
            }

            //-------------------------------------------------------------------------------------------------------------------------------
            Debug.Log("Return the plane: from heightLevel to runwayEndPoint.");
            while (Vector3.Distance(lentokone.transform.position, runwayEndPoint) > 0.5f) // Work Distance from heightLevel, current position to runwayEndPoint
            { 
                // Direction from current position to runwayEndPoint
                currentDirection = (runwayEndPoint - lentokone.transform.position).normalized;
                currentSpeed -= acceleration * Time.deltaTime * 0.2f; // Slowing down
                
                // Move the plane to runwayEndPoint with current speed
                lentokone.transform.position += currentDirection * currentSpeed * Time.deltaTime;

                // Smoothly turning the nose of the plane toward
                Quaternion targetRotation = Quaternion.LookRotation(currentDirection);
                lentokone.transform.rotation = Quaternion.Slerp(lentokone.transform.rotation, targetRotation, speedRotate * Time.deltaTime);
                
                yield return null;
            }  

            //-------------------------------------------------------------------------------------------------------------------------------
            Debug.Log("Stop the plane: from runwayEndPoint to startPoint.");
            while (Vector3.Distance(lentokone.transform.position, startPoint) > 0.5f) // Work Distance from runwayEndPoint to startPoint
            { 
                // Direction  from runwayEndPoint to startPoint
                currentDirection = (startPoint - lentokone.transform.position).normalized;
                currentSpeed -= acceleration * Time.deltaTime * 1.2f; // Slowing down
                
                /// Move the plane to startPoint with current speed
                lentokone.transform.position += currentDirection * currentSpeed * Time.deltaTime;

                // Smoothly turning the nose of the plane toward
                Quaternion targetRotation = Quaternion.LookRotation(currentDirection);
                lentokone.transform.rotation = Quaternion.Slerp(lentokone.transform.rotation, targetRotation, speedRotate * Time.deltaTime);
                
                yield return null;
            }

            //-------------------------------------------------------------------------------------------------------------------------------
            Debug.Log("Pause before new flight.");
            yield return new WaitForSeconds(pauseTime); // Pause before new flight

            //-------------------------------------------------------------------------------------------------------------------------------
            Debug.Log("Rotation in startPoint to startDirection.");        
            while (Quaternion.Angle(lentokone.transform.rotation, startDirection) > 0.5f)
            {
                lentokone.transform.rotation = Quaternion.Slerp(lentokone.transform.rotation, startDirection, speedRotate * Time.deltaTime * 0.2f);
                yield return null; 
            }
        }   
    }
}

