using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;

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
