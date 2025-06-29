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

	void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
