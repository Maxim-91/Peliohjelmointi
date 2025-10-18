using UnityEngine;

public class SmoothFollowCamera : MonoBehaviour
{
    public Transform target;      // игрок
    public Vector3 offset = new Vector3(0f, 3f, -5f); // позиция камеры относительно игрока
    public float followSpeed = 5f;
    public float rotateSpeed = 5f;

    void LateUpdate()
    {
        if (!target) return;

        // Желаемая позиция камеры
        Vector3 desiredPosition = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Поворот камеры за игроком
        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotateSpeed * Time.deltaTime);
    }
}
