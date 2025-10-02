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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameManeger.singleton.player;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRadius)

        {
            Vector2 direction = (player.position - transform.position).normalized;

            movement = new Vector2(direction.x, esVolador?direction.y:0);
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
            movement = Vector2.zero;
        }

        if (distanceToPlayer < distanceStop )
        {
            movement = Vector2.zero;
        }

        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }


}
