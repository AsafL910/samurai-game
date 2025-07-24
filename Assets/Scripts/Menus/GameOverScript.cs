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

    public void MainMenu()
    {
        PlayerState.shouldResetPlayer = true; // Reset the flag for future loads
        GameObject currentCanvas = GameObject.Find("GameOverScreen");
        if (currentCanvas != null)
        {
            currentCanvas.SetActive(false);
        }

        PlayerState.GetPlayerStatus().SetTransform(GameManager.instance.checkpoint);
        PlayerState.GetPlayerStatus().SetSceneIndex(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Resuming main menu from scene index: " + PlayerState.GetPlayerStatus().GetSceneIndex());
        PlayerState.GetPlayerStatus().SavePlayer();
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
