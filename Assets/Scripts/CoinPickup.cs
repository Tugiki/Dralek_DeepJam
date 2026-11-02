
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddCoinScore(10);
            }

            Debug.Log("Coin toplandý!");
            Destroy(gameObject);
        }
    }
}
