using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath aiPath;
    public float speed;

    void Update()
    {
        if (aiPath.desiredVelocity.x > 0.01f) {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        if (aiPath.desiredVelocity.x < -0.01f) {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }
}
