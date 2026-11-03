using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float lifeTime = 5f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void Launch(Vector2 direction)
    {
        rb.linearVelocity = direction.normalized * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth =
                other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}