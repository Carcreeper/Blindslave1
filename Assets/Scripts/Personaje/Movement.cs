using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Jobs;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] public float SpeedForce;
    [SerializeField] public InputActionProperty crouchAction; // Nueva acción para agacharse

    private Rigidbody2D rb;
    public Vector2 movent;
    public InputActionProperty inpM;
    public Transform graphics;
    public Jump jump;

    
    [SerializeField] private bool isCrouching = false;
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private Vector2 originalColliderSize;
    [SerializeField] private Vector2 originalColliderOffset;
    [SerializeField] private Vector2 crouchColliderSize = new Vector2(1f, 0.5f); // Tamaño del collider al agacharse
    [SerializeField] private Vector2 crouchColliderOffset = new Vector2(0f, -0.25f); // Offset del collider al agacharse

    private void Awake()
    {
        inpM.action.Enable();
        crouchAction.action.Enable();

        // Suscribirse a los eventos de agacharse
        crouchAction.action.started += StartCrouch;
        crouchAction.action.canceled += StopCrouch;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();

        // Guardar el tamaño y offset original del collider
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
        movent = inpM.action.ReadValue<Vector2>();

        // Solo permitir movimiento horizontal si no está agachado
        if (!isCrouching)
        {
            // Voltear el sprite según la dirección
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
        Vector2 movent2 = movent;
        movent2.y = 0;
        if (!jump.isGrounded)
        {
            movent2.x *= 0.5f;
        }

        // Si está agachado, no aplicar movimiento horizontal
        if (isCrouching)
        {
            movent2.x = 0;
        }

        rb.linearVelocity = (rb.linearVelocity.y * Vector2.up + movent2 * SpeedForce);
    }

    private void StartCrouch(InputAction.CallbackContext context)
    {
        if (!isCrouching)
        {
            isCrouching = true;
            AdjustColliderForCrouch(true);

            // Opcional: Ajustar la escala del sprite para mostrar visualmente que está agachado
            if (graphics != null)
            {
                graphics.localScale = new Vector3(graphics.localScale.x, 0.6f, graphics.localScale.z);
            }
        }
    }

    private void StopCrouch(InputAction.CallbackContext context)
    {
        if (isCrouching)
        {
            isCrouching = false;
            AdjustColliderForCrouch(false);

            // Restaurar la escala original del sprite
            if (graphics != null)
            {
                float xScale = graphics.localScale.x;
                graphics.localScale = new Vector3(xScale, 1f, graphics.localScale.z);
            }
        }
    }

    private void AdjustColliderForCrouch(bool crouch)
    {
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

    // Método público para verificar si el jugador está agachado (útil para otros scripts)
    public bool IsCrouching()
    {
        return isCrouching;
    }

    private void OnDestroy()
    {
        // Desuscribirse de los eventos para evitar errores
        if (crouchAction.action != null)
        {
            crouchAction.action.started -= StartCrouch;
            crouchAction.action.canceled -= StopCrouch;
        }
    }
}