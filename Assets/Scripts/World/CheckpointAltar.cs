using UnityEngine;
using UnityEngine.UI;
public class CheckpointAltar : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject mensajeUI;
    [SerializeField] private Text textoMensaje; // O TextMeshProUGUI si usas TextMesh Pro

    [Header("Configuración")]
    [SerializeField] private float rangoDeteccion = 3f;
    [SerializeField] private KeyCode teclaGuardar = KeyCode.E;
    [SerializeField] private Color colorActivado = Color.green;
    [SerializeField] private Color colorDesactivado = Color.white;

    private Transform jugador;
    private bool jugadorCerca = false;
    private bool checkpointActivado = false;
    private Renderer altarRenderer;

    void Start()
    {
        // Encuentra al jugador por tag
        GameObject jugadorObj = GameObject.FindGameObjectWithTag("Player");
        if (jugadorObj != null)
            jugador = jugadorObj.transform;

        // Obtiene el renderer del altar
        altarRenderer = GetComponent<Renderer>();

        // Oculta el mensaje al inicio
        if (mensajeUI != null)
            mensajeUI.SetActive(false);

        // Configura el texto
        if (textoMensaje != null)
            textoMensaje.text = "Pulsa E para guardar";
    }

    void Update()
    {
        if (jugador == null) return;

        // Calcula la distancia al jugador
        float distancia = Vector3.Distance(transform.position, jugador.position);

        // Verifica si el jugador está cerca
        if (distancia <= rangoDeteccion)
        {
            if (!jugadorCerca)
            {
                jugadorCerca = true;
                MostrarMensaje(true);
            }

            // Detecta la tecla E para guardar
            if (Input.GetKeyDown(teclaGuardar))
            {
                GuardarCheckpoint();
            }
        }
        else
        {
            if (jugadorCerca)
            {
                jugadorCerca = false;
                MostrarMensaje(false);
            }
        }
    }

    void GuardarCheckpoint()
    {
        // Guarda la posición del checkpoint
        GameManager.Instance.GuardarCheckpoint(transform.position);

        // Activa el checkpoint visualmente
        checkpointActivado = true;
        if (altarRenderer != null)
            altarRenderer.material.color = colorActivado;

        // Cambia el mensaje temporalmente
        if (textoMensaje != null)
        {
            textoMensaje.text = "¡Juego guardado!";
            Invoke("RestaurarMensaje", 2f);
        }

        Debug.Log("Checkpoint guardado en: " + transform.position);
    }

    void RestaurarMensaje()
    {
        if (textoMensaje != null)
            textoMensaje.text = "Pulsa E para guardar";
    }

    void MostrarMensaje(bool mostrar)
    {
        if (mensajeUI != null)
            mensajeUI.SetActive(mostrar);
    }

    // Visualiza el rango en el editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
    }
}
