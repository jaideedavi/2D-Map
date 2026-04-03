using UnityEngine;

public class PickUp : MonoBehaviour
{
   void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();

            if(inventory != null)
            {
                inventory.AddKeycard();
            }

            Destroy(gameObject);
        }
    }
}
