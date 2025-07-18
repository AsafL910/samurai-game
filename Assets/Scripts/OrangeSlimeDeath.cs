using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICustomEvent
{
	void OnCustomEvent(string data);
}

public class OrangeSlimeDeath : MonoBehaviour, ICustomEvent
{
	public GameObject poison;

	public void OnCustomEvent(string data)
	{
		if (data == "kill")
		{
			Instantiate(poison, transform.position, Quaternion.identity);
			FindObjectOfType<AudioManager>().Play("PoisonStart");
		}
	}
}
