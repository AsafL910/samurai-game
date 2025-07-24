using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float maxViewingDistance;
    void Start()
    {
    }

    private void Update()
    {
        if ((PlayerState.GetPlayerStatus().GetTransform() - transform.position).magnitude > maxViewingDistance)
        {
            gameObject.GetComponent<AIDestinationSetter>().target = null;
        }
        else
        {
            gameObject.GetComponent<AIDestinationSetter>().target.position = PlayerState.GetPlayerStatus().GetTransform();
        }
    }
}
