using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float maxViewingDistance;
    private PlayerStatus playerStatus;

    void Start()
    {
        playerStatus = PlayerState.GetPlayerStatus();
    }

    private void Update()
    {
        if ((playerStatus.GetTransform() - transform.position).magnitude > maxViewingDistance)
        {
            gameObject.GetComponent<AIDestinationSetter>().target = null;
        }
        else
        {
            gameObject.GetComponent<AIDestinationSetter>().target.position = playerStatus.GetTransform();
        }
    }
}
