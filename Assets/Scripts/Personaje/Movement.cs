using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField]public float SpeedForce;
    private Rigidbody2D rb;
    private Vector2 movent;
    public InputActionProperty inpM;

    private void Awake()
    {
        inpM.action.Enable();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        movent=inpM.action.ReadValue<Vector2>();    
        //movent.x = Input.GetAxisRaw("Horizontal");       
    }

    void FixedUpdate()
    {
        rb.linearVelocity=(rb.linearVelocity.y * Vector2.up + movent * SpeedForce);    
    }
    public void Damage()
    {
        Vector2 rebote = new Vector2(transform.position.x - Vector2.right.x, 1).normalized;
        rb.AddForce(rebote * 6, ForceMode2D.Impulse);
        Debug.Log("activé el daño en el movimiento");
    }
}
