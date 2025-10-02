using UnityEngine;
using UnityEngine.InputSystem;

public class Block : MonoBehaviour
{
    public InputActionProperty inpB;
    public bool isblocking;
    [Range(0f, 1f)] public float damageReduction;

    private void Awake()
    {
        inpB.action.Enable();
    }
    private void Update()
    {
        isblocking = inpB.action.ReadValue<float>()>0.5f;
    }
    public float ConvertDamage(float c) 
    {
        if (isblocking) 
        {
            
            return c * (1-damageReduction);

        } 
        return c;
    }



}
