using UnityEngine;

public class EnemyPassThrough : MonoBehaviour
{
    [SerializeField] private Collider2D enemyBodyCollider;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogWarning("Player bulunamadý!");
            return;
        }

        Collider2D playerCollider = player.GetComponent<Collider2D>();

        if (enemyBodyCollider == null || playerCollider == null)
        {
            Debug.LogWarning("Collider referansý eksik!");
            return;
        }

        Physics2D.IgnoreCollision(
            enemyBodyCollider,
            playerCollider,
            true
        );

        Debug.Log("Enemy oyuncunun içinden geçebilir!");
    }
}