using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    void Start()
    {
        EnableInGameUI(false);
    }

    public static void PlayGame()
    {
        SaveSystem.DeleteSave(); // Clear any existing save data
        PlayerState.SetPlayerStatus(new PlayerStatus());
        PlayerState.GetPlayerStatus().NewStart();
        GameManager.instance.checkpoint = PlayerState.GetPlayerStatus().GetTransform();
        if (PlayerMovement.instance != null)
        {
            PlayerMovement.instance.player.position = PlayerState.GetPlayerStatus().GetTransform();
        }
        SceneManager.LoadScene("Forest");
        EnableInGameUI(true);
        GameObject pauseMenu = GameObject.Find("Pause Menu");
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }

        if (PlayerState.shouldResetPlayer)
        {
            Debug.Log("Resetting player state due to shouldResetPlayer flag.");
            PlayerState.GetPlayerStatus().FillHP();
            var rds = FindObjectOfType<RecieveDamage>();
            if (rds != null)
            {
                rds.ResetState(PlayerState.GetPlayerStatus().GetTransform());
            }
            PlayerState.shouldResetPlayer = false;
        }

        Time.timeScale = 1;
    }

    public static void LoadGame()
    {
        PlayerState.SetPlayerStatus(new PlayerStatus());
        PlayerState.GetPlayerStatus().Start();
        Debug.Log("Loading game from checkpoint: " + PlayerState.GetPlayerStatus().GetTransform());
        GameManager.instance.checkpoint = PlayerState.GetPlayerStatus().GetTransform();
        if (PlayerMovement.instance != null)
        {
            PlayerMovement.instance.player.position = PlayerState.GetPlayerStatus().GetTransform();
        }

        SceneManager.LoadScene(PlayerState.GetPlayerStatus().GetSceneIndex());
        Debug.Log("Loading game from scene index: " + PlayerState.GetPlayerStatus().GetSceneIndex());
        EnableInGameUI(true);
        GameObject pauseMenu = GameObject.Find("Pause Menu");
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }

        if (PlayerState.shouldResetPlayer)
        {
            Debug.Log("Resetting player state due to shouldResetPlayer flag.");
            PlayerState.GetPlayerStatus().FillHP();
            var rds = FindObjectOfType<RecieveDamage>();
            if (rds != null)
            {
                rds.ResetState(PlayerState.GetPlayerStatus().GetTransform());
            }
            PlayerState.shouldResetPlayer = false;
        }

        Time.timeScale = 1;
    }

    static void EnableInGameUI(bool enable)
    {
        CameraScript.instance?.gameObject.SetActive(enable);
        GameObject pauseMenu = GameObject.Find("Pause Menu");
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(enable);
        }
        PauseMenuScript.instance?.setUIelements(enable);
    }

    public static void QuitGame()
    {
        Application.Quit();
    }
}
