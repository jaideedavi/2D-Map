using UnityEngine;

public class MissionItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();

            if (inventory != null)
            {
                inventory.hasMissionItem = true;
                Destroy(gameObject);
            }
        }
    }
}
