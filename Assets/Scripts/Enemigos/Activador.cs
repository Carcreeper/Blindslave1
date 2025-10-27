using UnityEngine;
using UnityEngine.Events;

public class Activador : MonoBehaviour
{
    public UnityEvent accion;
    
    public void Activar()
    {
        accion.Invoke();
    }
}
