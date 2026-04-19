using UnityEngine;

public class ZombieAI : MonoBehaviour
{
public float moveSpeed = 2f;

    [Header("Detection")]
    public Transform player;
    public float detectionRange = 10f;

    [Header("Timing")]
    public float minWalkTime = 2f;
    public float maxWalkTime = 5f;
    public float minIdleTime = 1f;
    public float maxIdleTime = 3f;

    private float timer;
    private float currentDuration;

    private Vector3 moveDirection;
    private bool isWalking;

    void Start()
    {
        PickNewState();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // 👇 CHASE PLAYER
        if (distanceToPlayer <= detectionRange)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

            if (direction != Vector3.zero)
                transform.forward = direction;

            return; // stop wandering when chasing
        }

        // 👇 NORMAL WANDER
        timer += Time.deltaTime;

        if (isWalking)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

            if (moveDirection != Vector3.zero)
                transform.forward = moveDirection;
        }

        if (timer >= currentDuration)
        {
            PickNewState();
        }
    }

    void PickNewState()
    {
        timer = 0f;

        isWalking = Random.value > 0.5f;

        if (isWalking)
        {
            currentDuration = Random.Range(minWalkTime, maxWalkTime);

            float x = Random.Range(-1f, 1f);
            float z = Random.Range(-1f, 1f);
            moveDirection = new Vector3(x, 0, z).normalized;
        }
        else
        {
            currentDuration = Random.Range(minIdleTime, maxIdleTime);
            moveDirection = Vector3.zero;
        }
    }
}
