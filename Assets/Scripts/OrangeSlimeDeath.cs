using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeSlimeDeath : MonoBehaviour
{
	public GameObject poison;
	private void OnDestroy()
	{
		Instantiate(poison, transform.position, Quaternion.identity);
		FindObjectOfType<AudioManager>().Play("PoisonStart");
	}
}
