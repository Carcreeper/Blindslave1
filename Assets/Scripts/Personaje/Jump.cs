using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] public float jumpForce;
    [SerializeField] public float doubleJumpForce;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;
    [SerializeField] float checkRadius;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool canDoubleJump;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);


        if (isGrounded) 
        { 
        
            canDoubleJump = true;
        
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            
            rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocityY + jumpForce );

        }
    }
}
