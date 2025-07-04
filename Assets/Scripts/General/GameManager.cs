using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public Vector2 checkpoint;

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
		// Register a one-time scene loaded callback
		SceneManager.sceneLoaded += OnSceneLoaded;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		SceneManager.sceneLoaded -= OnSceneLoaded; // Unregister to avoid duplicate calls
		Debug.Log("OnSceneLoaded called");
		var player = FindObjectOfType<RecieveDamage>();
		if (player != null)
		{
			Debug.Log("Reset state called");
			player.ResetState(checkpoint);
		}
	}
}
