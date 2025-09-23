using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Slider sliderHealth;
    public Health health;

    private void Update()
    {
        if (sliderHealth == null || health == null)
        {
            Debug.LogError("Falta inicializar componentes para el slider de la salud");
            return;
        }

        sliderHealth.value = health.health / health.maxHealth;
    }


}
