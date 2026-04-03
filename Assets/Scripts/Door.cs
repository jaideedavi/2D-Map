using UnityEngine;

public class Door : MonoBehaviour
{public float openHeight = 4f;
    public float speed = 2f;

    private Vector3 closedPos;
    private Vector3 openPos;
    private bool opening = false;

    void Start()
    {
        closedPos = transform.position;
        openPos = closedPos + new Vector3(0, openHeight, 0);
    }

    void Update()
    {
        if (opening)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                openPos,
                speed * Time.deltaTime
            );
        }
    }

    public void OpenDoor()
    {
        opening = true;
    }
}
