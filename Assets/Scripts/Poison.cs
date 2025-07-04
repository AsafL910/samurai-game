using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour
{
    public float damage;

    public void Start()
    {
        StartCoroutine(SelfDestroy());
    }

    public IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
