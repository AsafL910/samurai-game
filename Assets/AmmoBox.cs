using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public GameObject ammoPrefab;
    public GameObject breakablePrefab;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "sword")
        {
            BreakBox();
        }
    }
    public void BreakBox()
    {
        var ammo = Instantiate(ammoPrefab);
        var debris = Instantiate(breakablePrefab);
        ammo.transform.position = transform.position;
        debris.transform.position = transform.position;

        AudioManager.instance.Play("Box Break");
        Destroy(gameObject);
    }
}
