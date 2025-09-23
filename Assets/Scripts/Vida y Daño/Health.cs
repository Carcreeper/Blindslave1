using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Movement movement;
    public bool isDeath;

    [ContextMenu("Causar daño")]
    public void TestDamage()
    {
        TakeDamage(1);
    }
    public void TakeDamage(float c)
    {
        health -= c;
        if (health <= 0)
        {
            Death();
        }

        if (movement != null)
        {
            movement.Damage();
        }


    }
    public void Death ()
    {
 
        isDeath = true;

    }
}
