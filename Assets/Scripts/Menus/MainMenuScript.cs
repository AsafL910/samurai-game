using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public static void PlayGame()
    {
        SceneManager.LoadScene("Forest");
        Time.timeScale = 1;
    }

    public static void QuitGame()
    {
        Application.Quit();
    }
}
