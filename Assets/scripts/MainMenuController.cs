using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public AudioSource sfxSource;

    public AudioClip clickSFX;
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("game");
        sfxSource.PlayOneShot(clickSFX);
        Invoke("LoadScene", 0.5f);
    }
    public void QuitGame()
    {
        sfxSource.PlayOneShot(clickSFX);
        Invoke("Quit", 0.5f);
        Application.Quit();
    }
}