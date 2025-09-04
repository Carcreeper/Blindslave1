using Unity.VisualScripting;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] public float jumpForce;
    [SerializeField] public float doubleJumpForce;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;
    [SerializeField] float checkRadius;

    private Rigidbody2D rb;
    [SerializeField] private bool isGrounded;
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
            Debug.Log("isgrounded");
        
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {

            rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocityY + jumpForce);

        }
        else if (Input.GetKeyDown(KeyCode.Space)&& canDoubleJump && !isGrounded) 
        { 
        
            rb.linearVelocity= new Vector2(rb.linearVelocityX,doubleJumpForce);
            canDoubleJump= false;
            
        }



    }
        //gizmovisual
        
    void OnDrawGizmos() { 
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
            
    }
}
