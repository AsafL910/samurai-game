using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayActivation : MonoBehaviour
{
    public float seconds;
    void Start()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        StartCoroutine(EnableCollision());
    }

    IEnumerator EnableCollision()
    {
        yield return new WaitForSeconds(seconds);
        GetComponent<CircleCollider2D>().enabled = true;
    }
}
