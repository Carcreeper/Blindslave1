using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Vector3 ultimoCheckpoint;
    private bool hayCheckpointGuardado = false;
    public Transform player;
    void Awake()
    {
        // Patrón Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GuardarCheckpoint(Vector3 posicion)
    {
        ultimoCheckpoint = posicion;
        hayCheckpointGuardado = true;

        // Guardar en PlayerPrefs para persistencia entre sesiones
        PlayerPrefs.SetFloat("CheckpointX", posicion.x);
        PlayerPrefs.SetFloat("CheckpointY", posicion.y);
        PlayerPrefs.SetFloat("CheckpointZ", posicion.z);
        PlayerPrefs.SetInt("HayCheckpoint", 1);
        PlayerPrefs.Save();

        Debug.Log("Checkpoint guardado en PlayerPrefs");
    }

    public Vector3 CargarCheckpoint()
    {
        if (PlayerPrefs.HasKey("HayCheckpoint"))
        {
            float x = PlayerPrefs.GetFloat("CheckpointX");
            float y = PlayerPrefs.GetFloat("CheckpointY");
            float z = PlayerPrefs.GetFloat("CheckpointZ");

            ultimoCheckpoint = new Vector3(x, y, z);
            hayCheckpointGuardado = true;

            return ultimoCheckpoint;
        }

        return Vector3.zero;
    }

    public bool TieneCheckpointGuardado()
    {
        return hayCheckpointGuardado || PlayerPrefs.HasKey("HayCheckpoint");
    }

    public void RespawnearEnCheckpoint(GameObject jugador)
    {
        if (TieneCheckpointGuardado())
        {
            Vector3 posicionRespawn = CargarCheckpoint();
            jugador.transform.position = posicionRespawn;
            Debug.Log("Jugador respawneado en: " + posicionRespawn);
        }
        else
        {
            Debug.LogWarning("No hay checkpoint guardado");
        }
    }
}
