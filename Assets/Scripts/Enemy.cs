using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyHealth = 100;

    public GameObject keycardPrefab;
    public Transform dropPoint;

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
