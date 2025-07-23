using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public GameObject gameOverScreen;
    private void Start()
    {
        gameOverScreen.SetActive(false);
    }
    public void ResumeGame()
    {
        gameOverScreen.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Restart()
    {
        gameOverScreen.SetActive(false);
        Time.timeScale = 1f;
        GameManager.instance.Restart();
    }

    public static void MainMenu()
    {
        GameObject currentCanvas = GameObject.Find("Canvas");
        if (currentCanvas != null)
        {
            currentCanvas.SetActive(false);
        } 
        SceneManager.LoadScene("Menu");
    }

    public static void QuitGame()
    {
        Application.Quit();
    }
}
