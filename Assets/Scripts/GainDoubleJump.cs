using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainDoubleJump : MonoBehaviour
{
    public GameObject DoubleJumpTutorialMessage;

    void Start()
    {
        gameObject.SetActive(!PlayerState.GetPlayerStatus().CanDoubleJump());
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerStatus>().SetCanDoubleJump(true);
            DoubleJumpTutorialMessage.SetActive(true);
            AudioManager.instance.Play("GainAbility");
            Destroy(gameObject);
        }
    }
}
