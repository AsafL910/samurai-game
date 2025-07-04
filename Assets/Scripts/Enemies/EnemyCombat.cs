using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public EnemyStatus enemyStatus;
    public GameObject sparks;
    public GameObject cameraShake;
    public BloodSplatter bloodSplatter;
    public RipplePostProcessor ripplePostProcessor;
    public string[] soundsWhenHit;

    public float maxHealth;
    public float hitpoints;
    public float weight;
    public float knockBack;
    public float damage;

    public AIDestinationSetter aIDestinationSetter;

    void Start()
    {
        ripplePostProcessor = FindObjectOfType<RipplePostProcessor>();
        enemyStatus = new EnemyStatus(maxHealth, hitpoints, weight, knockBack, damage);
        aIDestinationSetter.target = FindObjectOfType<PlayerMovement>().gameObject.transform;
    }

    public void DeathCheck()
    {
        if (enemyStatus.hitpoints <= 0f)
        {
            ripplePostProcessor.Ripple();
            // cameraShake.GetComponent<CameraShake>().Shake();
            //fIX
            Instantiate(bloodSplatter.GetRandomSplatter(), transform.position - new Vector3(0, 0.6f, 0), Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "sword")
        {
            FindObjectOfType<AudioManager>().Play(soundsWhenHit[Random.Range(0, soundsWhenHit.Length)]);
            TakeSwordDamage(other.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "sword")
        {
            FindObjectOfType<AudioManager>().Play(soundsWhenHit[Random.Range(0, soundsWhenHit.Length)]);
            TakeSwordDamage(collision.collider.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Poison"))
        {
            hitpoints -= Time.deltaTime * other.gameObject.GetComponent<Poison>().damage;
        }
    }

    void TakeSwordDamage(GameObject sword)
    {
        Sword impact = sword.GetComponent<Sword>();
        Vector2 hitDirection = impact.direction;
        float damage = impact.damage;
        this.GetComponent<Rigidbody2D>().AddForce(hitDirection.normalized * (2000f / this.enemyStatus.weight), ForceMode2D.Force);
        TakeAnyDamage(damage);
    }

    public void TakeAnyDamage(float damage)
    {
        enemyStatus.hitpoints -= damage;
        Instantiate(sparks, transform.position, Quaternion.identity);
        DeathCheck();
    }
}
