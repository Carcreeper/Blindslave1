using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Atack : MonoBehaviour
{
    public float attackForce;
    public float attackDelay;
    public float attackRange;
    public Transform attackPivot;
    public InputActionProperty atk;
    public LayerMask enemyLayer;
    private bool isAttacking;

    private void Awake()
    {
        atk.action.Enable();
        atk.action.performed += AttackMethod;
    }

    private void OnDestroy()
    {
        atk.action.performed -= AttackMethod;
    }


    IEnumerator Attacking()
    {
        Debug.Log("estoyatacando");
        isAttacking = true;
        yield return new WaitForSeconds(attackDelay);
        Vector2 attackPoint = new Vector2(attackPivot.position.x, attackPivot.position.y);
        Collider2D c2d = Physics2D.OverlapCircle(attackPoint, attackRange, enemyLayer);
        if (c2d != null)
        {
            Health health = c2d.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(attackForce);
            }
            yield return new WaitForSeconds(attackDelay);
        }
        isAttacking=false;
    }

    public void AttackMethod(InputAction.CallbackContext cnt) 
    {
        if (!isAttacking) 
        {
        
            StartCoroutine(Attacking());
        
        }
    
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if (!isAttacking) Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPivot.position, attackRange);

    }
}
