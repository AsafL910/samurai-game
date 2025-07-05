using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
	public string toScene;
	public Vector3 destination;
	public void LoadScene()
	{
		StartCoroutine(GameManager.instance.LoadLevel(toScene, destination));
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			LoadScene();
		}
	}
}
