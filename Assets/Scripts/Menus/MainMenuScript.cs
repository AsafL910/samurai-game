using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public static void PlayGame()
    {
        SceneManager.LoadScene("Forest");
        GameManager.instance.checkpoint = new Vector3(-36.5f, 10, 0);
        Time.timeScale = 1;
    }

    public static void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
