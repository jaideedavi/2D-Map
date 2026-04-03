using UnityEngine;

public class KeyCardDoor : MonoBehaviour
{
    public Transform door;
    public float openHeight = 3f;
    public float openSpeed = 2f;

    private bool opening = false;

    private Vector3 startPosition;
    private Vector3 targetPosition;

    void Start()
    {
        startPosition = door.position;
        targetPosition = startPosition + Vector3.up * openHeight;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();

            if(inventory != null && inventory.hasKeycard)
            {
                opening = true;
            }
            else
            {
                Debug.Log("Door locked. Need keycard.");
            }
        }
    }

    void Update()
    {
        if(opening)
        {
            door.position = Vector3.MoveTowards(
                door.position,
                targetPosition,
                openSpeed * Time.deltaTime
            );
        }
    }
}