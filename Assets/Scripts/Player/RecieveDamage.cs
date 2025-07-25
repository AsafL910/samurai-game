using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RecieveDamage : MonoBehaviour
{
    public GameObject blood;
    public GameObject katana;
    public GameObject gameOverScreen;
    public bool gameEnded;
    [SerializeField]
    private float deathDelay;
    void Start()
    {
        gameEnded = false;
        PlayerState.GetPlayerStatus().FillHP();
    }
    void Update()
    {
        if (PlayerState.GetPlayerStatus().GetHP() <= 0 && !gameEnded && !PlayerState.shouldResetPlayer)
        {
            Debug.Log(PlayerState.GetPlayerStatus().GetHP() + " Player HP at death." + gameEnded + "." + PlayerState.shouldResetPlayer);
            Debug.Log("Player has died, triggering game over sequence.");
            //play death animation
            GameManager.EndSessionAndSave();

            Instantiate(katana, transform.position, Quaternion.identity);
            Instantiate(blood, transform.position, Quaternion.identity);
            gameEnded = true;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerCombat>().enabled = false;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            GetComponent<CapsuleCollider2D>().enabled = false;

            FindObjectOfType<AudioManager>().Stop("Ambient Music - Mossy");
            FindObjectOfType<AudioManager>().Stop("footsteps");
            FindObjectOfType<AudioManager>().Play("DeathSound");
            Invoke("ShowGameOverScreen", deathDelay);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("InstantKill"))
        {
            PlayerState.GetPlayerStatus().TakeDamage(PlayerState.GetPlayerStatus().GetTotalHP());
        }
    }
    void ShowGameOverScreen()
    {
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "EnemyShuriken")
        {
            PlayerState.GetPlayerStatus().SetHP(PlayerState.GetPlayerStatus().GetHP() - 20);
        }
    }

    public void ResetState(Vector3 checkpoint)
    {
        Debug.Log("Resetting player state to checkpoint: " + checkpoint);

        gameEnded = false;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<PlayerCombat>().enabled = true;
        GetComponent<Rigidbody2D>().constraints ^= RigidbodyConstraints2D.FreezePositionX;
        GetComponent<Rigidbody2D>().constraints ^= RigidbodyConstraints2D.FreezePositionY;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        GetComponent<CapsuleCollider2D>().enabled = true;
        FindObjectOfType<AudioManager>().Play("Ambient Music - Mossy");

        PlayerState.GetPlayerStatus().FillHP();
        Debug.Log("Player HP reset to: " + PlayerState.GetPlayerStatus().GetHP());
        transform.position = checkpoint;
    }
}
