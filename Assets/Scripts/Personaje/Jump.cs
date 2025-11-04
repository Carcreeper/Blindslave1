using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    [SerializeField] public float jumpForce;
    [SerializeField] public float doubleJumpForce;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;
    [SerializeField] float checkRadius;
    public InputActionProperty jumpAction;

    public Health health;
    private Rigidbody2D rb;
    public bool isGrounded;
    private bool canDoubleJump;

   
    private Movement movementScript;

    private void Awake()
    {
        jumpAction.action.Enable();
        rb = GetComponent<Rigidbody2D>();
        movementScript = GetComponent<Movement>();
        jumpAction.action.performed += JumpMethod;
    }

    void Start()
    {

    }

    void Update()
    {
        if (health != null && health.isDeath) return;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        if (isGrounded)
        {
            canDoubleJump = true;
        }
    }

    public void JumpMethod(InputAction.CallbackContext cnt)
    {
        if (health != null && health.isDeath) return;

        if (movementScript != null && movementScript.IsCrouching())
        {
            return; 
        }

        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        else if (canDoubleJump && !isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, doubleJumpForce);
            canDoubleJump = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
}