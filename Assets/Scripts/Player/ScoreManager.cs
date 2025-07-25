using UnityEngine;
using UnityEngine.SceneManagement;

public static class ScoreManager
{
    public static void UpdateTotalScore(string sceneIndex)
    {
        var count =
        (GameObject.FindObjectOfType<GenerateEnemies>()?.maxEnemyCount ?? 0) +
        GameObject.FindObjectsOfType<SlimeMovement>().Length +
        CountEntitiesWithTag("ShurikenBox");
        Debug.Log("update totalscore "+count);
        var sceneIdentifier = "visited_scene_" + sceneIndex.ToString();

        if (!PlayerPrefs.HasKey(sceneIdentifier))
        {
            PlayerPrefs.SetInt(sceneIdentifier, 1);
            PlayerPrefs.SetInt("totalscore", count + PlayerPrefs.GetInt("totalscore", 0));
            PlayerPrefs.Save();
        }

    }

    public static int CountEntitiesWithTag(string tag)
    {
        return GameObject.FindGameObjectsWithTag(tag).Length;
    }

    public static void UpdateScore(GameObject obj)
    {
        var identifier = obj.scene.buildIndex.ToString() + obj.name;
        if (!PlayerPrefs.HasKey(identifier))
        {
            Debug.Log("add score");
            PlayerPrefs.SetInt(identifier, 1);
            PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 1);
            PlayerPrefs.Save();
        }
    }
}
