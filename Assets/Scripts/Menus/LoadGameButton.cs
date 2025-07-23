using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGameButton : MonoBehaviour
{
    public Button loadGameButton;
    public void Awake()
    {
        string path = Application.persistentDataPath + "/player.lumer";

        if (File.Exists(path))
        {
            loadGameButton.interactable = true;
        }
        else
        {
            loadGameButton.interactable = false;
        }
    }
}