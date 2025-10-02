using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Jobs;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] public float SpeedForce;
    [SerializeField] public InputActionProperty crouchAction; 

    private Rigidbody2D rb;
    public Vector2 movent;
    public InputActionProperty inpM;
    public Transform graphics;
    public Jump jump;
    public float dashForce;
    public InputActionProperty inpD;
    public bool isDashing;
    public float timeDashing;
    public Health health;

    [SerializeField] private bool isCrouching = false;
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private Vector2 originalColliderSize;
    [SerializeField] private Vector2 originalColliderOffset;
    [SerializeField] private Vector2 crouchColliderSize = new Vector2(1f, 0.5f); 
    [SerializeField] private Vector2 crouchColliderOffset = new Vector2(0f, -0.25f); 

    private void Awake()
    {
        inpM.action.Enable();
        crouchAction.action.Enable();
        inpD.action.Enable();
       
        crouchAction.action.started += StartCrouch;
        crouchAction.action.canceled += StopCrouch;
        inpD.action.performed += Dash;
    }

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();

        
        if (playerCollider != null)
        {
            if (playerCollider is BoxCollider2D boxCollider)
            {
                originalColliderSize = boxCollider.size;
                originalColliderOffset = boxCollider.offset;
            }
            else if (playerCollider is CapsuleCollider2D capsuleCollider)
            {
                originalColliderSize = capsuleCollider.size;
                originalColliderOffset = capsuleCollider.offset;
            }
        }
    }

    void Update()
    {
        if (health != null && health.isDeath) return;
        movent = inpM.action.ReadValue<Vector2>();
        
        
        if (!isCrouching)
        {
            
            if (movent.x > 0)
            {
                graphics.localScale = Vector3.one;
            }
            else if (movent.x < 0)
            {
                graphics.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    void FixedUpdate()
    {
        if (health != null && health.isDeath) return;

        Vector2 movent2 = movent;
        movent2.y = 0;
        if (!jump.isGrounded)
        {
            movent2.x *= 0.5f;
        }

        
        if (isCrouching)
        {
            movent2.x = 0;
        }

        if (!isDashing)
        {

            rb.linearVelocity = (rb.linearVelocity.y * Vector2.up + movent2 * SpeedForce);

        }
        else 
        {        
            
            rb.linearVelocity = graphics.localScale.x * Vector3.right * dashForce + rb.linearVelocity.y * Vector3.up;

        }

    }

    private void StartCrouch(InputAction.CallbackContext context)
    {
        if (health != null && health.isDeath) return;

        if (!isCrouching)
        {
            isCrouching = true;
            AdjustColliderForCrouch(true);

            
            if (graphics != null)
            {
                graphics.localScale = new Vector3(graphics.localScale.x, 0.6f, graphics.localScale.z);
            }
        }
    }

    private void StopCrouch(InputAction.CallbackContext context)
    {
        if (health != null && health.isDeath) return;

        if (isCrouching)
        {
            isCrouching = false;
            AdjustColliderForCrouch(false);

           
            if (graphics != null)
            {
                float xScale = graphics.localScale.x;
                graphics.localScale = new Vector3(xScale, 1f, graphics.localScale.z);
            }
        }
    }

    private void AdjustColliderForCrouch(bool crouch)
    {
        if (health != null && health.isDeath) return;

        if (playerCollider == null) return;

        if (playerCollider is BoxCollider2D boxCollider)
        {
            if (crouch)
            {
                boxCollider.size = crouchColliderSize;
                boxCollider.offset = crouchColliderOffset;
            }
            else
            {
                boxCollider.size = originalColliderSize;
                boxCollider.offset = originalColliderOffset;
            }
        }
        else if (playerCollider is CapsuleCollider2D capsuleCollider)
        {
            if (crouch)
            {
                capsuleCollider.size = crouchColliderSize;
                capsuleCollider.offset = crouchColliderOffset;
            }
            else
            {
                capsuleCollider.size = originalColliderSize;
                capsuleCollider.offset = originalColliderOffset;
            }
        }
    }

    public void Damage()
    {
        Vector2 rebote = new Vector2(transform.position.x - Vector2.right.x, 1).normalized;
        rb.AddForce(rebote * 6, ForceMode2D.Impulse);
        Debug.Log("activó el daño en el movimiento");
    }


    public void Dash(InputAction.CallbackContext c)
    {
        
        isDashing = true;
       
        Invoke("UnableDash",timeDashing);
        

    }

    public void UnableDash()
    {
        isDashing = false;
    }

    public bool IsCrouching()
    {
        return isCrouching;
    }

    private void OnDestroy()
    {
        
        if (crouchAction.action != null)
        {
            crouchAction.action.started -= StartCrouch;
            crouchAction.action.canceled -= StopCrouch;
        }
    }

}