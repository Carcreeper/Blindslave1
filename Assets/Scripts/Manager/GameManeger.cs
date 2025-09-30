using UnityEngine;

public class GameManeger : MonoBehaviour
{
    public static GameManeger singleton;
    public Transform player;
    public Health playerHealth;
    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }

        else
        {
            DestroyImmediate(gameObject);
        }
    }


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
