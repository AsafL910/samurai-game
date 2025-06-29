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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public static void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
