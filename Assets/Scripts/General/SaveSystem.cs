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
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);

            PlayerStatus data = formatter.Deserialize(stream) as PlayerStatus;
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }

    public static void LoadGame()
    {
        MainMenuScript.PlayGame();
    }

}
