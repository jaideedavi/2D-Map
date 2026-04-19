using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasKeycard = false;

    public bool hasMissionItem = false;

    public void AddKeycard()
    {
        hasKeycard = true;
        Debug.Log("Keycard collected!");
    }
}
