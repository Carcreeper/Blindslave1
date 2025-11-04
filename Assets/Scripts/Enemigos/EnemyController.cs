using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform player;
    public float detectionRadius = 5.0f;
    public float speed = 2.0f;
    public float distanceStop;
    private Rigidbody2D rb;
    private Vector2 movement;
    public bool esVolador;
    public Health health;
    public Collider2D collider;
    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameManager.singleton.player;
    }

    // Update is called once per frame
    void Update()
    {
        if (health != null && health.isDeath) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRadius)

        {
            animator.SetBool("Caminando", true);
            Vector2 direction = (player.position - transform.position).normalized;

            movement = new Vector2(direction.x, esVolador ? direction.y : rb.linearVelocity.y);

            //ROTACION DEL ENEMIGO
            if (direction.x > 0)
            {
               transform.localRotation = Quaternion.Euler (0, 180, 0);
            }
            else if (direction.x < 0) 
            {
               transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            //ROTACION DEL ENEMIGO
        }

        else 
        {
            animator.SetBool("Caminando", false);
            movement = new Vector2(0, rb.linearVelocity.y);
        }

        if (distanceToPlayer < distanceStop )
        {
            rb.MovePosition(new Vector2(rb.position.x + movement.x * speed * Time.deltaTime, rb.position.y));
        }

        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }  

    public void Morir()
    {
        animator.SetBool("Muriendo", true);
        if (collider!= null)
        {
            collider.enabled = false;

        }

        Destroy(rb);
        this.enabled = false;
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }


}
