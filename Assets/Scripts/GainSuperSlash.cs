using UnityEngine;

public class GainSuperSlash : MonoBehaviour
{
    public GameObject SuperSlashTutorialMessage;
    public GameObject DoubleJumpTutorialMessage;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerStatus>().SetCanSuperSlash(true);
            SuperSlashTutorialMessage.SetActive(true);
            DoubleJumpTutorialMessage.SetActive(false);
            Destroy(gameObject);
        }
    }
}
