using UnityEngine;
using System.Collections;
public class EnemyAttack : MonoBehaviour
{
    public float attackForce;
    public float attackDelay;
    public float attackRange;
    public Transform attackPivotEnemy;
    private bool isEnemyAttacking;
 


    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, GameManeger.singleton.player.position);


        if (distanceToPlayer < attackRange)
        {
            if (distanceToPlayer < attackRange && !isEnemyAttacking)
            {
                isEnemyAttacking = true;
                StartCoroutine(AttackPlayer());
                
            }
        }
    }


    private IEnumerator AttackPlayer()
    {
        while (isEnemyAttacking)
        {

            yield return new WaitForSeconds(attackDelay);

            GameManeger.singleton.playerHealth.TakeDamage(attackForce);


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










