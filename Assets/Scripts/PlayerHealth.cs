
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;

    private int currentHealth;
    private bool isDead;

    [SerializeField] private TMP_Text healthText;

    private void Awake()
    {
        Time.timeScale = 1f;

        currentHealth = maxHealth;
        isDead = false;

        UpdateHealthUI();
        Debug.Log("Oyuncunun caný: " + currentHealth);
    }

    public void TakeDamage(int damage)
    {
        if(isDead)
        {
            return;
        }

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        UpdateHealthUI();

        Debug.Log("Oyuncu hasar aldý! Kalan can: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Oyuncunun caný bitti! Oyun durduruldu.");
        Time.timeScale = 0f;
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Can: " + currentHealth + " / " + maxHealth;
        }
    }
}

