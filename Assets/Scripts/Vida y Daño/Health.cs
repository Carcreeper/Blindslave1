using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Health : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Movement movement;
    public bool isDeath;
    public UnityEvent eventoMorir;
    public float tiempoEsperar = 3f;
    public bool isPlayer ;

    [ContextMenu("Causar da�o")]
    public void TestDamage()
    {
        TakeDamage(1);
    }
    public void TakeDamage(float c)
    {
        if (isDeath)
        {
            return;
        }
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
        eventoMorir.Invoke();
        isDeath = true;
        if (!isPlayer)
        {
            if (movement != null)
            {
                movement.enabled = false;
            }

            StartCoroutine(EsperarYDesaparecer());
        }
        else 
        { 

            GameManager.Instance.RespawnearEnCheckpoint(gameObject);

        }

    }

    private IEnumerator EsperarYDesaparecer()
    {
        yield return new WaitForSeconds(tiempoEsperar);
        gameObject.SetActive(false); 
    }
}
