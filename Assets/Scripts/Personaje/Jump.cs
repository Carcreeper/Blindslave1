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

    private Rigidbody2D rb;
    [SerializeField] private bool isGrounded;
    private bool canDoubleJump;

    private void Awake()
    {

        jumpAction.action.Enable();
        rb = GetComponent<Rigidbody2D>();
        jumpAction.action.performed += JumpMethod;
    }

    void Start()
    {

    }

    
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);


        if (isGrounded) 
        { 
        
            canDoubleJump = true;
            
        
        }

       



    }
    //gizmovisual

    public void JumpMethod(InputAction.CallbackContext cnt)
    {

        if (isGrounded)
        {

            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);

        }
        else if (
            
            canDoubleJump && !isGrounded)
        {

            rb.linearVelocity = new Vector2(rb.linearVelocityX, doubleJumpForce);
            canDoubleJump = false;

        }

    }
        
    void OnDrawGizmos() { 
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
            
    }
}
