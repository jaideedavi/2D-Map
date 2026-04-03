using UnityEngine;

public class Button : MonoBehaviour
{
      public Door door;

    void OnMouseDown()
    {
        door.OpenDoor();
    }
}
