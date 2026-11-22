
using UnityEngine;

public class FlyingEnemyMovement : MonoBehaviour
{
    [Header("Horizontal Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float leftLimit = -5.5f;
    [SerializeField] private float rightLimit = 5.5f;

    [Header("Flying Movement")]
    [SerializeField] private float waveHeight = 0.7f;
    [SerializeField] private float waveSpeed = 2f;

    private Rigidbody2D rb;
    private float startY;
    private float timeOffset;
    private float direction = 1f;
    private Vector3 originalScale;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        startY = rb.position.y;
        timeOffset = Random.Range(0f, 10f);
        originalScale = transform.localScale;
    }

    private void FixedUpdate()
    {
        float newX = rb.position.x +
            direction * moveSpeed * Time.fixedDeltaTime;

        if (newX >= rightLimit)
        {
            newX = rightLimit;
            direction = -1f;
        }
        else if (newX <= leftLimit)
        {
            newX = leftLimit;
            direction = 1f;
        }

        Vector3 newScale = originalScale;
        newScale.x = Mathf.Abs(originalScale.x) * direction;

        transform.localScale = newScale;

        float newY = startY +
            Mathf.Sin((Time.time + timeOffset) * waveSpeed) * waveHeight;

        rb.MovePosition(new Vector2(newX, newY));
    }
}
