using Unity.Mathematics;
using UnityEngine;

public class cinemachine : MonoBehaviour
{
    public new Vector2 rangeX, rangeY;
    public Transform target;
    public float velocidad;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 nPose = Vector3.Lerp(transform.position,target.position, velocidad*Time.deltaTime);
        nPose.x = Mathf.Clamp(nPose.x,rangeX.x,rangeX.y);
        nPose.y = Mathf.Clamp(nPose.y, rangeY.x, rangeY.y);
        transform.position = nPose;
    }
}
