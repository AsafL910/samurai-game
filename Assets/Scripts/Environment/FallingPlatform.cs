using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
	[SerializeField]
	private Rigidbody2D rb;
	[SerializeField]
	private float fallDelay;

	[SerializeField]
	private GameObject brokenRockParts;
	private void Start()
	{
		gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
		rb.isKinematic = true;
	}
	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player" && other.gameObject.transform.position.y > transform.position.y) {
			StartCoroutine(fall());
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "ground")
		{
			FindObjectOfType<AudioManager>().Play("BreakingRocks");
			Instantiate(brokenRockParts, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}

	private IEnumerator fall() {

		yield return new WaitForSeconds(fallDelay);
		gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
		rb.isKinematic = false;
	}
}
