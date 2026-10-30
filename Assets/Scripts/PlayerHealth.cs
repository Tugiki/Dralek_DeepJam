
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;

    private int currentHealth;
    private bool isDead;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private int flashCount = 3;
    private float flashInterval = 0.05f;

    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private GameObject restartButton;

    private void Awake()
    {
        Time.timeScale = 1f;

        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }

        currentHealth = maxHealth;
        isDead = false;

        UpdateHealthUI();

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }

        if (restartButton != null)
        {
            restartButton.SetActive(false);
        }

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

        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
            CancelInvoke(nameof(ResetColor));
            CancelInvoke(nameof(FlashPlayer));
            flashCount = 3;
            Invoke(nameof(FlashPlayer), flashInterval);
        }

        Debug.Log("Oyuncu hasar aldý! Kalan can: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
        }

        if (restartButton != null)
        {
            restartButton.SetActive(true);
        }

        Debug.Log("Oyuncunun caný bitti! Oyun durduruldu.");

        Time.timeScale = 0f;
    }

    private void UpdateHealthUI()
    {
        if (healthText == null)
        {
            return;
        }

        healthText.text = "Can: " + currentHealth + " / " + maxHealth;

        if (currentHealth > maxHealth / 2f)
        {
            healthText.color = Color.green;
        }
        else if (currentHealth > 1)
        {
            healthText.color = Color.yellow;
        }
        else
        {
            healthText.color = Color.red;
        }
    }

    private void FlashPlayer()
    {
        if (spriteRenderer == null || isDead)
        {
            return;
        }

        spriteRenderer.enabled = !spriteRenderer.enabled;

        flashCount--;

        if (flashCount > 0)
        {
            Invoke(nameof(FlashPlayer), flashInterval);
        }
        else
        {
            spriteRenderer.enabled = true;
            ResetColor();
            flashCount = 3;
        }
    }

    private void ResetColor()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
    }
}

