using UnityEngine;

public class DoorWithKeycard : MonoBehaviour
{
  public Animator doorAnimator;
    public AudioSource audioSource;

    public bool isOpen = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isOpen) return;

        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();

            if (inventory != null && inventory.hasKeycard)
            {
                doorAnimator.SetTrigger("Open");

                // 🔊 Play sound
                if (audioSource != null)
                    audioSource.Play();

                isOpen = true;
            }
            else
            {
                Debug.Log("Door locked - need keycard");
            }
        }
    }
}
