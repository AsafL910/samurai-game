using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public Vector2 checkpoint;

	public Animator sceneTransitionAnimation;

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

	public void Restart()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
		Debug.Log("OnSceneLoaded called");
		var player = FindObjectOfType<RecieveDamage>();
		if (player != null)
		{
			Debug.Log("Reset state called");
			player.ResetState(checkpoint);
		}
	}

	public IEnumerator LoadLevel(string scene, Vector3? adjustedPosition)
	{
		checkpoint = new Vector2(adjustedPosition?.x ?? 0, adjustedPosition?.y ?? 0);
		sceneTransitionAnimation.SetTrigger("End");
		yield return new WaitForSeconds(0.4f);
		SceneManager.LoadSceneAsync(scene);
		sceneTransitionAnimation.SetTrigger("Start");
		ScoreManager.UpdateTotalScore(scene);
		if (adjustedPosition != null && adjustedPosition != Vector3.zero)
		{
			yield return new WaitForSeconds(0.8f);
		}
	}

	private static float sessionStartTime = -1f;

    public static void StartSession()
    {
        sessionStartTime = Time.time;
    }

    public static void EndSessionAndSave()
    {
        if (sessionStartTime < 0f) return;

        float sessionDuration = Time.time - sessionStartTime;
        float totalTime = PlayerPrefs.GetFloat("totalPlayTime", 0f);
        PlayerPrefs.SetFloat("totalPlayTime", totalTime + sessionDuration);
        PlayerPrefs.Save();
        Debug.Log("Total play time now: " + (totalTime + sessionDuration) + " seconds");
    }

	public static string GetTotalPlayTime()
	{
		float totalSeconds = PlayerPrefs.GetFloat("totalPlayTime", 0f);
		int hours = Mathf.FloorToInt(totalSeconds / 3600);
		int minutes = Mathf.FloorToInt(totalSeconds % 3600 / 60);
		int seconds = Mathf.FloorToInt(totalSeconds % 60);
		return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
	}
	public static void ResetTotalPlayTime()
	{
		PlayerPrefs.DeleteKey("totalPlayTime");
		PlayerPrefs.Save();
		Debug.Log("Total play time has been reset.");
	}

}
