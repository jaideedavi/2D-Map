using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyHealth = 100;

    public int damageToPlayer = 10;
    public float damageCooldown = 1f;

    private float lastDamageTime;

    public GameObject keycardPrefab;
    public Transform dropPoint;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time >= lastDamageTime + damageCooldown)
            {
                PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();

                if (player != null)
                {
                    player.TakeDamage(damageToPlayer);
                    lastDamageTime = Time.time;
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;

        if (enemyHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        DropKeycard();
        Destroy(gameObject);
    }

    void DropKeycard()
    {
        if (keycardPrefab != null)
        {
            Instantiate(keycardPrefab, dropPoint.position, Quaternion.identity);
        }
    }
   
}
