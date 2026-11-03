using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float leftLimit = -5.5f;
    [SerializeField] private float rightLimit = 5.5f;

    private Rigidbody2D rb;
    private int direction = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = (Random.Range(0, 2) == 0) ? -1 : 1;
    }

    private void FixedUpdate()
    {
        if (transform.position.x <= leftLimit)
        {
            direction = 1;
        }
        else if (transform.position.x >= rightLimit)
        {
            direction = -1;
        }

        Vector3 enemyScale = transform.localScale;
        enemyScale.x = Mathf.Abs(enemyScale.x) * direction;
        transform.localScale = enemyScale;

        rb.linearVelocity = new Vector2(
            direction * moveSpeed,
            rb.linearVelocity.y
        );
    }
}