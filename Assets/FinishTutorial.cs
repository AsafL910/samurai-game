using UnityEngine;

public class FinishTutorial : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerMovement.instance.playerStatus.sawTutorial = true;
        }
    }
}
