using UnityEngine;

public class GainSuperSlash : MonoBehaviour
{
    public GameObject SuperSlashTutorialMessage;
    public GameObject DoubleJumpTutorialMessage;

    void Start()
    {
        gameObject.SetActive(!PlayerState.GetPlayerStatus().CanSuperSlash());
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerState.GetPlayerStatus().SetCanSuperSlash(true);
            SuperSlashTutorialMessage.SetActive(true);
            DoubleJumpTutorialMessage.SetActive(false);
            AudioManager.instance.Play("GainAbility");
            Destroy(gameObject);
        }
    }
}
