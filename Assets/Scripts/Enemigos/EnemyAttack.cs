using UnityEngine;
using System.Collections;
public class EnemyAttack : MonoBehaviour
{
    public float attackForce;
    public float attackDelay;
    public float attackRange;
    public Transform player;
    public Health playerHealth;
    public Transform attackPivotEnemy;
    private bool isEnemyAttacking;
    private Vector2 movement;


    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);


        if (distanceToPlayer < attackRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            if (distanceToPlayer < attackRange && !isEnemyAttacking)
            {
                isEnemyAttacking = true;
                StartCoroutine(AttackPlayer());
                movement = Vector2.zero;
            }
            else if (distanceToPlayer < attackRange)
            {
                movement = direction;

            }

            else
            {
                isEnemyAttacking = false;
            }


        }
    }


    private IEnumerator AttackPlayer()
    {
        while (isEnemyAttacking)
        {

            yield return new WaitForSeconds(attackDelay);

            playerHealth.TakeDamage(attackForce);


            yield return new WaitForSeconds(attackDelay);
            isEnemyAttacking = false;

        }

    }
    private void OnDrawGizmosSelected()

    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}










