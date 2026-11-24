
using UnityEngine;

public class EnemyRise : MonoBehaviour
{
    [Header("Rise Settings")]
    [SerializeField] private float riseDistance = 3f;
    [SerializeField] private float riseSpeed = 3f;

    private Rigidbody2D rb;
    private Collider2D enemyCollider;
    private EnemyPatrol enemyPatrol;

    private float targetY;
    private float originalGravityScale;
    private bool originalIsTrigger;
    private bool isRising = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<Collider2D>();
        enemyPatrol = GetComponent<EnemyPatrol>();

        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = false;
        }

        if (rb != null)
        {
            originalGravityScale = rb.gravityScale;
            rb.gravityScale = 0f;
            rb.linearVelocity = Vector2.zero;
        }

        if (enemyCollider != null)
        {
            originalIsTrigger = enemyCollider.isTrigger;
            enemyCollider.isTrigger = true;
        }
    }

    private void Start()
    {
        targetY = transform.position.y + riseDistance;
    }

    private void FixedUpdate()
    {
        if (!isRising || rb == null)
        {
            return;
        }

        float nextY = Mathf.MoveTowards(
            rb.position.y,
            targetY,
            riseSpeed * Time.fixedDeltaTime
        );

        rb.MovePosition(new Vector2(rb.position.x, nextY));

        if (Mathf.Abs(nextY - targetY) < 0.02f)
        {
            rb.position = new Vector2(rb.position.x, targetY);
            rb.linearVelocity = Vector2.zero;

            isRising = false;

            if (enemyCollider != null)
            {
                enemyCollider.isTrigger = originalIsTrigger;
            }

            rb.gravityScale = originalGravityScale;

            if (enemyPatrol != null)
            {
                enemyPatrol.enabled = true;
            }

            Debug.Log("Enemy zemine ulaþtý ve devriyeye baþladý!");
        }
    }
}
