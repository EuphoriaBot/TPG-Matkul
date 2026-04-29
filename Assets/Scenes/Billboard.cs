using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform camTrasform;
    
    void Start()
    {
        camTrasform = Camera.main.transform;
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + camTrasform.forward);
    }
}
