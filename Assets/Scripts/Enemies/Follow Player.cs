using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float maxViewingDistance;
    public PlayerStatus playerStatus;

    private void Update()
    {
        if ((playerStatus.GetTransform().position - transform.position).magnitude > maxViewingDistance)
        {
            gameObject.GetComponent<AIDestinationSetter>().target = null;
        }
        else
        {
            gameObject.GetComponent<AIDestinationSetter>().target = playerStatus.GetTransform();
        }
    }
}
