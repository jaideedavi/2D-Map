using UnityEngine;

public class CardMovement : MonoBehaviour
{
    public float rotateSpeed = 60f;

    void Update()
    {
        transform.Rotate(Vector3.left * rotateSpeed * Time.deltaTime);
    }
}
