using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Key : MonoBehaviour
{

    public AIDestinationSetter aIDestinationSetter;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            aIDestinationSetter.target = collision.gameObject.transform;
        }
    }
}
