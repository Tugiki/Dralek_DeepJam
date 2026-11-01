
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private GameObject coinPrefab;

    private int currentHealth;

    private void Awake()  // OYUN BAÞINDA CANI MAXHEALTH EÞÝTLEME
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {   // CANI AZALMA VE 0 ÝLE LÝMÝTLEME
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        Debug.Log(
            gameObject.name + " hasar aldý! Kalan can: " + currentHealth
        );

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    // OBJEYÝ YOK ETME
    private void Die()
    {

        if (coinPrefab != null)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Coin Prefab atanmamýþ!");
        }

        Debug.Log(gameObject.name + " yok edildi!");
        Destroy(gameObject);
    }
}

