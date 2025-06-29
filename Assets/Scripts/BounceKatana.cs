using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceKatana : MonoBehaviour
{
    [SerializeField]
    private float force;
    private Rigidbody2D rb;
    private AudioManager audioManager;
    private int dropCounter;
    void Start()
    {
        dropCounter = 0;
        audioManager = FindObjectOfType<AudioManager>();
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other)
    { 
        if (other.gameObject.tag == "ground" && !audioManager.isPlaying("SwordDrop") && dropCounter < 1)
        {
           dropCounter++;
           audioManager.Play("SwordDrop");
        }
    }
}
