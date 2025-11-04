using UnityEngine;
using System.Collections;
public class EnemyAttack : MonoBehaviour
{
    public float attackForce;
    public float attackDelay;
    public float attackRange;
    public Transform attackPivotEnemy;
    private bool isEnemyAttacking;
    public Health health;
    public Animator animator;


    private void Update()
    {
        if (health != null && health.isDeath) return;

        float distanceToPlayer = Vector2.Distance(transform.position, GameManager.singleton.player.position);

        if (distanceToPlayer < attackRange)
        {
            if (distanceToPlayer < attackRange && !isEnemyAttacking)
            {
                isEnemyAttacking = true;
                StartCoroutine(AttackPlayer());
            }
        }

    }

    public void Atacar()
    {
        GameManager.singleton.playerHealth.TakeDamage(attackForce);
    }
    private IEnumerator AttackPlayer()
    {
       
        while (isEnemyAttacking)
        {
            animator.SetBool("Atacando", true);
            yield return new WaitForSeconds(attackDelay);
            yield return new WaitForSeconds(attackDelay);
            isEnemyAttacking = false;

            if (health != null && health.isDeath) yield break;
        }
        animator.SetBool("Atacando", false);
    }
    private void OnDrawGizmosSelected()

    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}










