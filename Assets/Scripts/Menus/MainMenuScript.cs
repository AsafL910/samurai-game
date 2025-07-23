using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    void Start()
    {
        Debug.Log("MainMenuScript started.");
    }

    public static void PlayGame()
    {
        PlayerState.SetPlayerStatus(new PlayerStatus());
        PlayerState.GetPlayerStatus().Start();
        SceneManager.LoadScene("Forest");
        Time.timeScale = 1;
    }

    public static void QuitGame()
    {
        Application.Quit();
    }
}
