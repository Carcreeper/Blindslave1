using UnityEngine;
using UnityEngine.UI;
public class MenuCalidad : MonoBehaviour
{
    public Dropdown dropdown;
    public int calidad;

    private void Start()
    {
        calidad = PlayerPrefs.GetInt("numeroDeCalidad", 3);
        dropdown.value = calidad;
        AjustarCalidad();
    }
    public void AjustarCalidad()
    {
        QualitySettings.SetQualityLevel(dropdown.value);
        PlayerPrefs.SetFloat("Brillo", dropdown.value);
        calidad = dropdown.value;
    }

}
