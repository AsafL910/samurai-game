using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RecieveDamage : MonoBehaviour
{
    public PlayerStatus player;
    public GameObject blood;
    public GameObject katana;
    public GameObject gameOverScreen;
    public bool gameEnded;
    [SerializeField]
    private float deathDelay;
    void Start()
    {
        gameEnded = false;
        player.FillHP();
    }
    void Update()
    {
        if (player.GetHP() <= 0 && !gameEnded)
        {
            //play death animation
            Instantiate(katana, transform.position, Quaternion.identity);
            Instantiate(blood, transform.position, Quaternion.identity);
            gameEnded = true;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<PlayerMovement>().enabled = false;
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
            player.TakeDamage(player.GetTotalHP());
        }
    }
    void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            player.SetHP(player.GetHP() - 20);
        }
    }
    
    public void ResetState(Vector3 checkpoint)
    {
        gameEnded = false;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<PlayerStatus>().FillHP();
        transform.position = checkpoint;
    }
}
