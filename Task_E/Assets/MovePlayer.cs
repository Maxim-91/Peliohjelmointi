using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float moveSpeed = 5f; // speed of movement

    void Update()
    {
        // Получаем ввод по осям
        float moveX = Input.GetAxis("Horizontal"); // A/D or left/right arrows
        float moveZ = Input.GetAxis("Vertical");   // W/S or forward/backward arrows

        // Create a motion vector
        Vector3 move = new Vector3(moveX, 0f, moveZ);

        // Move the object
        transform.Translate(-move * moveSpeed * Time.deltaTime, Space.Self);
    }
}
