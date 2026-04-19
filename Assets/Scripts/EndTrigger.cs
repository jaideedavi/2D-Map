using UnityEngine;
using UnityEngine.SceneManagement;


public class EndTrigger : MonoBehaviour
{
public string missionCompleteSceneName = "MissionComplete";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();

            if (inventory != null && inventory.hasMissionItem)
            {
                SceneManager.LoadScene(missionCompleteSceneName);
            }
            else
            {
                Debug.Log("You need the mission item!");
            }
        }
    }
}
