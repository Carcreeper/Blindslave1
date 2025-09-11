using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField]public float SpeedForce;
    private Rigidbody2D rb;
    private Vector2 movent;
    public InputActionProperty inpM;


    private bool recibiendoDaño;
    public float fuerzaRebote;

    

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
        movent.x = Input.GetAxisRaw("Horizontal");       
    }

    void FixedUpdate()
    {
        rb.linearVelocity=(rb.linearVelocity + movent * SpeedForce);    
    }

    //DAÑO
    public void RecibeDaño(Vector2 direccion, int cantidadDeDaño)
    {
        if (!recibiendoDaño)
        {
            recibiendoDaño = true;
            Vector2 rebote = new Vector2(transform.position.x - direccion.x, 1).normalized;
            rb.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse);
        }
    }
        public void DesactivandoDaño()
        {
            recibiendoDaño=false;
            rb.linearVelocity= Vector2.zero;
        }




}

