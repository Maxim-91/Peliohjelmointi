/* Used transform.rotation to rotate the object. 
Everything worked perfect until the next stage "jump".
I wanted to use physics for a more natural jump. I didn't want to raise and lower the object using transform.position.
For help, I used Google Gemini AI and rewrote the code to use Rigidbody.
There was an issue with the object jumping and continuing to rotate, or not jumping at all, related to the "IsGrounded()" ground sense.
AI Chat GPT recommended using WaitForSeconds(). */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PukinpyoritysJaHyppy : MonoBehaviour
{
    private GameObject joulupukki;
    private Rigidbody joulupukkiRB;
    private float startAngleY = 0f; // Start spin rotation, Y axis
    private float endAngleY = 360f; // Full spin rotation, Y axis
    private float endTimeRotation = 5f;
    private float currentTime = 0f;
    
    // Variables for checking the ground under your feet joulupukki for IsGrounded()
    public Transform groundCheck;   // What point to check - it is GameObject terrain
    public float groundRadius = 0.4f; // Radius of the inspection sphere, lets be 0.4f
    public LayerMask groundLayer;   // The layer that is the "ground" - it is "Maa" in GameObject terrain

    void Awake()
    {
        joulupukki = GameObject.Find("joulupukki"); // Etsi kentästä Joulupukki
    }
    void Start()
    {         
        joulupukkiRB = joulupukki.GetComponent<Rigidbody>();  
        joulupukkiRB.MoveRotation(Quaternion.identity); // This is rotation = (0,0,0)

        // The object is on a mountain and there are slight vibrations up and down when rotating
        // X and Z are frozen in the settings, Y will be manipulated when necessary
        joulupukkiRB.constraints |= RigidbodyConstraints.FreezePositionY;

        StartCoroutine(PyoritysJaHyppy());
    }

    IEnumerator PyoritysJaHyppy()
    {
        while (true)
        {
            yield return Rotate360();
            yield return Jump();            
        }
    }

    IEnumerator Rotate360()
    {
        Debug.Log("Start rotation 360.");

        currentTime = 0f;
        joulupukkiRB.constraints |= RigidbodyConstraints.FreezePositionY;       

        // A Smooth loop rotation in time 5 sec from startAngleY to endAngleY
        while (currentTime < endTimeRotation)
        { 
            currentTime += Time.deltaTime;

            float t = currentTime / endTimeRotation; // t from 0 to 1 - where 1 is end of rotation time
            float currentAngleY = Mathf.Lerp(startAngleY, endAngleY, t);
            joulupukkiRB.MoveRotation(Quaternion.Euler(0, currentAngleY, 0));

            yield return null;
        }

        // Forced return to end rotation point, after "full rotation", in case of small inaccuracies
        joulupukkiRB.MoveRotation(Quaternion.Euler(0, endAngleY, 0));
    }

    IEnumerator Jump()
    {
        Debug.Log("Jump Up and Down.");

        joulupukkiRB.constraints &= ~RigidbodyConstraints.FreezePositionY; // Release Y position
        joulupukkiRB.AddForce(Vector3.up * 10f, ForceMode.Impulse); // Use AddForce and ForceMode.Impulse to make the jump more natural

        // Wait until joulupukki touches the ground
        while (!IsGrounded()) yield return null;

        yield return new WaitForSeconds(2f); // Wait for 2 second before the next rotation and jump cycle
    }

    bool IsGrounded()
    {
        // Checks for colliders in the small sphere below the joulupukki
        // which are on the specified Layer "Maa" in GameObject "terrain"
        return Physics.CheckSphere(groundCheck.position, groundRadius, groundLayer);
    }  
}

