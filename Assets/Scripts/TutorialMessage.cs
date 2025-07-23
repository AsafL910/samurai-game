using UnityEngine;

public class TutorialMessage : MonoBehaviour
{
    public float removeMessageAfterSeconds = 1f;
    void Start()
    {
        gameObject.SetActive(!PlayerState.GetPlayerStatus().HasSeenTutorial());
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject, removeMessageAfterSeconds);
        }
    }
}
