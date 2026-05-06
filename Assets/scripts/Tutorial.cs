using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private bool isTutorialActive = true;

    private void Start()
    {
        UiController.Instance.ShowTutorial();
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (isTutorialActive && Input.GetKeyDown(KeyCode.Return))
        {
            UiController.Instance.HideTutorial();
            Time.timeScale = 1f;
            isTutorialActive = false;
        }
    }
}
