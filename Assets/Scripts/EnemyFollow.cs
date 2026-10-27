
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float followRange = 5f;
    [SerializeField] private float stopDistance = 1f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (target == null || rb == null)
        {
            return;
        }

        float distance = Vector2.Distance(
            rb.position,
            target.position
        );

        if (distance <= followRange && distance > stopDistance)
        {
            float directionX = Mathf.Sign(
                target.position.x - rb.position.x
            );

            rb.linearVelocity = new Vector2(
                directionX * moveSpeed,
                rb.linearVelocity.y
            );
        }
        else
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        }
    }
}
