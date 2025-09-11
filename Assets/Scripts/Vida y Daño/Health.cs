using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Movement movement;
    public bool isDeath;
    public void TakeDamage(float c)
    {
        health -= c;
        if (health <= 0)
        {
            Death();
        }

        if (movement != null)
        {
            //movement.Dagame()
        }
    }

    public void Death ()
    {
 
        isDeath = true;

    }
}
