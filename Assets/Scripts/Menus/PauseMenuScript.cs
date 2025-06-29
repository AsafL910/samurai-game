using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public TimeManager timeManager;
    public GameObject pauseMenuUI;
    public GameObject healthBarFill;
    public GameObject healthBar;
    public GameObject Resolve;
    public GameObject ResolveFill;
    private AudioManager audioManager;

    public static PauseMenuScript instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
	{
        audioManager = FindObjectOfType<AudioManager>();

    }
	private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuUI.activeSelf)
            {               
                ResumeGame();
            }
            else
            {            
                PauseGame();
            }
        }      
    }

    public void PauseGame()
    {
        audioManager.Play("ActiveMenu");
        audioManager.ChangeVolume("Ambient Music - Mossy", 0.1f);
        pauseMenuUI.SetActive(true);
        timeManager.StopTime();
        setUIelements(false);
    }

    public void ResumeGame()
    {
        audioManager.Play("ActiveMenu");
        audioManager.ChangeVolume("Ambient Music - Mossy", 0.2f);
        pauseMenuUI.SetActive(false);
        timeManager.RevertTime();
        setUIelements(true);  
    }
    public void ResumeMainMenu()
    {
        audioManager.Play("MenuButtonClick");
        SceneManager.LoadScene(0);
    }

    public void setUIelements(bool flag) 
    {
        healthBar.SetActive(flag);
        healthBarFill.SetActive(flag);
        Resolve.SetActive(flag);
        ResolveFill.SetActive(flag);
    }
}
