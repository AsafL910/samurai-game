using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{
    public static void Save(PlayerStatus player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.lumer";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, player);

        stream.Close();
    }

    public static PlayerStatus Load()
    {
        string path = Application.persistentDataPath + "/player.lumer";

        if (File.Exists(path))
        {
            Debug.Log("Loading player data from: " + path);
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerStatus data = formatter.Deserialize(stream) as PlayerStatus;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }

    public static void LoadGame()
    {
        MainMenuScript.PlayGame();
    }

    public static void DeleteSave()
    {
        GameManager.ResetTotalPlayTime();
        string path = Application.persistentDataPath + "/player.lumer";
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Save file deleted successfully.");
        }
        else
        {
            Debug.Log("No save file found to delete.");
        }
    }

}
