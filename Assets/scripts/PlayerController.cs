using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    public void die()
    {
        characterController.enabled = false;
        StartCoroutine(GameManager.Instance.Gameover());
    }
    
}
