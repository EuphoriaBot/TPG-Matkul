using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform camTrasform;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camTrasform = Camera.main.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(transform.position + camTrasform.forward);
    }
}
