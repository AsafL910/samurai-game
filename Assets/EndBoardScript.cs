using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndBoardScript : MonoBehaviour
{
    public GameObject endPanel;

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnReturnToMenu()
    {
        endPanel.SetActive(false);
        PlayerState.GetPlayerStatus().SetTransform(GameManager.instance.checkpoint);
        PlayerState.GetPlayerStatus().SetSceneIndex(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Resuming main menu from scene index: " + PlayerState.GetPlayerStatus().GetSceneIndex());
        PlayerState.GetPlayerStatus().SavePlayer();
        SceneManager.LoadScene(0);
    }
}
