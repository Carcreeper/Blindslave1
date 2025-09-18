using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Jobs;
[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField]public float SpeedForce;
    private Rigidbody2D rb;
    public Vector2 movent;
    public InputActionProperty inpM;
    public Transform graphics;

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
        if (movent.x>0)
        {
            graphics.localScale = Vector3.one;
        }
        else if(movent.x < 0)
        {
            graphics.localScale = new Vector3(-1,1,1);
        }
    }

    void FixedUpdate()
    {
        Vector2 movent2 = movent;
        movent2.y=0;
        rb.linearVelocity=(rb.linearVelocity.y * Vector2.up + movent2 * SpeedForce);    
    }
    public void Damage()
    {
        Vector2 rebote = new Vector2(transform.position.x - Vector2.right.x, 1).normalized;
        rb.AddForce(rebote * 6, ForceMode2D.Impulse);
        Debug.Log("activ� el da�o en el movimiento");
    }
}
