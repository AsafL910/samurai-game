using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatter : MonoBehaviour
{
	//public Dictionary<string, GameObject> effects;
	public GameObject bloodSplatter1;
	public GameObject bloodSplatter2;
	public GameObject bloodSplatter3;

	public GameObject GetRandomSplatter() {
		GameObject[] splatters = new GameObject[] {bloodSplatter1, bloodSplatter2, bloodSplatter3 };
		int randomIndex = Random.Range(0, 3);
		return splatters[randomIndex];
	}
	
}
