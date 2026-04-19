using UnityEngine;

public class KeyCard : MonoBehaviour
{
     private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();

            if (inventory != null)
            {
                inventory.hasKeycard = true;
                Destroy(gameObject);
            }
        }
    }
}
