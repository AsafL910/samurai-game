using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public static void PlayGame() {
        SceneManager.LoadScene("Forest 2");
        Time.timeScale = 1;
    }

    public static void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
