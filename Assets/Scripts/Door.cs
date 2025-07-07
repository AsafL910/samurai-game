using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject DoorObject;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Key")
        {
            DoorObject.GetComponent<Animator>().SetBool("IsOpen", true);
            AudioManager.instance.Play("InsertKey");
            AudioManager.instance.Play("StoneDoorOpen");
            Destroy(collision.gameObject);
        }
    }
}
