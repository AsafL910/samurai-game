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
		sceneTransitionAnimation.SetTrigger("End");
		yield return new WaitForSeconds(0.4f);
		SceneManager.LoadSceneAsync(scene);
		sceneTransitionAnimation.SetTrigger("Start");
		if (adjustedPosition != null && adjustedPosition != Vector3.zero)
		{
			yield return new WaitForSeconds(0.8f);
			FindObjectOfType<PlayerMovement>().gameObject.transform.position = (Vector3)adjustedPosition;
		}
	}
}
