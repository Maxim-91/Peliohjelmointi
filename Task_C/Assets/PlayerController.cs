using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        float move = 0f;
        float turn = 0f;

        if (Input.GetKey(KeyCode.W)) move = 1f;
        if (Input.GetKey(KeyCode.S)) move = -1f;
        if (Input.GetKey(KeyCode.A)) turn = -1f;
        if (Input.GetKey(KeyCode.D)) turn = 1f;

        // Поворот вокруг оси Y
        Quaternion rotation = Quaternion.Euler(0f, turn * rotationSpeed * Time.fixedDeltaTime, 0f);
        rb.MoveRotation(rb.rotation * rotation);

        // Движение вперёд/назад
        Vector3 movement = transform.forward * move * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }
}
