using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class ChangeKeyDestination : MonoBehaviour
{
    public Transform door;
    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Key")
        {
            var key = collision.gameObject;
            key.GetComponent<AIDestinationSetter>().target = door;
            key.GetComponent<Animator>().SetBool("ReachedDoor", true);
            var aiPath = key.GetComponent<AIPath>();
            aiPath.slowdownDistance = 0f;
            aiPath.endReachedDistance = 0f;
            aiPath.maxSpeed = 10f;
        }
    }
}
