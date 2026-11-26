
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float slamSpeed = 20f;

    private Rigidbody2D rb;
    private Animator animator;
    private float horizontalInput;
    private bool isGrounded;
    private bool isGroundSlamming;
    private float facingDirection = 1f;

    public float FacingDirection => facingDirection;
    public bool IsGroundSlamming => isGroundSlamming;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        horizontalInput = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.RightArrow))
        {
            horizontalInput = 1f;
            facingDirection = 1f;
        }
        else if (Input.GetKeyDown(KeyCode.A) ||
                 Input.GetKeyDown(KeyCode.LeftArrow))
        {
            horizontalInput = -1f;
            facingDirection = -1f;
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded &&
            !isGroundSlamming)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpForce
            );

            isGrounded = false;
        }

        if (!isGrounded && !isGroundSlamming &&
            (Input.GetKeyDown(KeyCode.S) ||
             Input.GetKeyDown(KeyCode.DownArrow)))
        {
            StartGroundSlam();
        }

        if (animator != null)
        {
            animator.SetFloat(
                "Speed",
                isGroundSlamming ? 0f : Mathf.Abs(horizontalInput)
            );

            animator.SetBool("IsGrounded", isGrounded);
        }
    }

    private void FixedUpdate()
    {
        if (isGroundSlamming)
        {
            rb.linearVelocity = new Vector2(0f, -slamSpeed);
            return;
        }

        rb.linearVelocity = new Vector2(
            horizontalInput * moveSpeed,
            rb.linearVelocity.y
        );
    }

    private void StartGroundSlam()
    {
        isGroundSlamming = true;
        rb.linearVelocity = new Vector2(0f, -slamSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            isGrounded = true;

            if (isGroundSlamming)
            {
                isGroundSlamming = false;
                rb.linearVelocity = Vector2.zero;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            isGrounded = false;
        }
    }
}
