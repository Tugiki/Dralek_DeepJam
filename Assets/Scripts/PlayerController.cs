using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;

    private Rigidbody2D rb;
    private Animator animator;
    private float horizontalInput;
    private bool isGrounded;
    private float facingDirection = 1f;

    public float FacingDirection => facingDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        horizontalInput = 1f;
    }

    private void Update()
    {
        // Yön tuþuna basýldýðýnda hareket yönünü deðiþtir.
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            horizontalInput = 1f;
            facingDirection = 1f;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            horizontalInput = -1f;
            facingDirection = -1f;
        }

        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
            animator.SetBool("IsGrounded", isGrounded);
        }

        // ZIPLAMA HAREKETÝ VE KOSULU
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {   // TUSA BASILDIÐINDA HAREKET ETME
        rb.linearVelocity = new Vector2(
            horizontalInput * moveSpeed,
            rb.linearVelocity.y
        );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {                                                                   //ZEMINLE TEMAS KONTROLU
        if (collision.gameObject.name == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {   //ZEMINLE TEMAS ETMEME KONTROLU
        if (collision.gameObject.name == "Ground")
        {
            isGrounded = false;
        }
    }
}
