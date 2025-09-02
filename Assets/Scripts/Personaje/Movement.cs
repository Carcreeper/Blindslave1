using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField]public float SpeedForce;
    private Rigidbody2D rb;
    private Vector2 movent;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        movent.x = Input.GetAxis("Horizontal");       
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movent * SpeedForce * Time.fixedDeltaTime);    
    }
}
