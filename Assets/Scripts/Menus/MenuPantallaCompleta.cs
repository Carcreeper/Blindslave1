using UnityEngine;
using UnityEngine.UI;

public class MenuPantallaCompleta : MonoBehaviour
{
    public Toggle toggle;

    private void Start()
    {
        if (Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }
    }

    public void ActiveFullScreen(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }
}
