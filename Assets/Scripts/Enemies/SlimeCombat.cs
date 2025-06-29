using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeCombat : EnemyCombat
{
    [SerializeField]
    private GameObject slime;
	void DeathCheck() {
        if (enemyStatus.hitpoints <= 0f)
        {
            ripplePostProcessor.Ripple();
            //cameraShake.GetComponent<CameraShake>().Shake();
            //fIX
            Instantiate(bloodSplatter.GetRandomSplatter(), transform.position, Quaternion.identity);
            Instantiate(slime, transform.position + Vector3.right.normalized, Quaternion.identity);
            Instantiate(slime, transform.position + Vector3.left.normalized, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
