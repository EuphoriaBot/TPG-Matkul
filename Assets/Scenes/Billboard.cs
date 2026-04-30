using UnityEngine;
using UnityEngine.UIElements;

public class Billboard : MonoBehaviour
{
    private Transform camTransform;
    
    void Start()
    {
        camTransform = Camera.main.transform;
    }

    void LateUpdate()
    {
        transform.forward = new Vector3(camTransform.forward.x, 0, camTransform.forward.z);
    }
}
