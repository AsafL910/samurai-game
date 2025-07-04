using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public PlayerMovement playerMovement;
    private Animator playerAnimationController;
    private Rigidbody2D rb;
    public PlayerStatus playerStatus;
    public float swordOffset;
    public CapsuleCollider2D swordCollider;
    public LayerMask enemyLayer;
    public PostProcessingEffects postProcessingEffects;
    private AudioManager audioManager;

    public Material matWhite;
    public Material matDefault;
    public SpriteRenderer spriteRenderer;
    private bool playerIsHit = false;
    private float flashDuration = 0.15f;
    public GameObject healEffect;

    public Vector3 hitDirection;
    public Animator swordAnimatorController;
    public GameObject Sword;
    public Sword swordScript;
    private GameObject Arrow => SlashArrowRotationScript.instance != null
    ? SlashArrowRotationScript.instance.gameObject
    : null;
    public TimeManager timeManager;
    public float slashSpeed;
    private bool isReadyForSlash;
    public static bool isSuperSlashing;
    private Vector2 superSlashDirection;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        spriteRenderer.material = matDefault;
        isSuperSlashing = false;
        rb = playerMovement.rb;
        playerAnimationController = playerMovement.playerAnimationController;
        Sword.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !playerMovement.isGrounded && playerStatus.CanSuperSlash() && playerStatus.isResolveFull())
        {
            if (!audioManager.isPlaying("SlowMo") && !isReadyForSlash)
            {
                audioManager.Play("SlowMo");
                audioManager.ChangeVolume("Ambient Music - Mossy", 0.05f);
            }

            Arrow.GetComponent<SpriteRenderer>().enabled = true;

            timeManager.SlowTime();
            playerMovement.playerAnimationController.SetBool("isSlowMotion", true);
            isReadyForSlash = true;
            superSlashDirection = Arrow.GetComponent<SlashArrowRotationScript>().GetDirection();
        }

        if (isReadyForSlash)
        {
            SuperSlash();
        }

        if (isSuperSlashing && playerMovement.isGrounded)
        {
            rb.velocity = Vector2.zero;
            isSuperSlashing = false;
            playerMovement.playerCollider.isTrigger = false;
        }

        if (Input.GetKeyDown(KeyCode.E) && !swordAnimatorController.GetCurrentAnimatorStateInfo(0).IsName("Slash1"))
        {
            StartCoroutine("Slash");
        }

        if (Input.GetKeyDown(KeyCode.R) && playerStatus.CanHeal() && playerStatus.isResolveFull())
        {
            heal();
        }
    }

    IEnumerator Slash()
    {
        audioManager.Play("SwordMiss");
        Sword.SetActive(true);
        playerAnimationController.SetBool("isSlash", true);
        float lookDirection = Input.GetAxisRaw("Vertical");
        Sword.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);

        if (lookDirection != 0)
        {
            hitDirection = new Vector3(0f, lookDirection * transform.localScale.x).normalized;
            changeSwordPosition(hitDirection * transform.localScale.x * 2);
            changeSwordRotation(hitDirection);
            swordScript.direction = -hitDirection;
        }

        else
        {
            hitDirection = new Vector3(playerMovement.transform.localScale.x, 0f).normalized;
            changeSwordPosition(hitDirection);
            swordScript.direction = hitDirection;
        }

        swordAnimatorController.Play("Slash1");

        yield return new WaitForSeconds(swordAnimatorController.GetCurrentAnimatorStateInfo(0).length);
        playerAnimationController.SetBool("isSlash", false);
        Sword.SetActive(false);
    }

    void SuperSlash()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift) || playerMovement.isGrounded)
        {
            audioManager.Play("SuperSlashPrepare");
            audioManager.ChangeVolume("Ambient Music - Mossy", 0.2f);
            StartCoroutine(postProcessingEffects.flashScreen());
            Arrow.GetComponent<SpriteRenderer>().enabled = false;
            playerAnimationController.SetBool("isSlowMotion", false);
            timeManager.RevertTime();

            if (!playerMovement.isGrounded)
            {
                rb.velocity = Vector2.zero;
                superSlashDirection.Normalize();
                rb.AddForce(superSlashDirection * slashSpeed, ForceMode2D.Impulse);

                if (superSlashDirection.x * playerMovement.horizontalMove < 0)
                {
                    playerMovement.FlipCharacter();
                }
            }

            isSuperSlashing = true;
            playerMovement.playerCollider.isTrigger = true;
            isReadyForSlash = false;

            playerStatus.SetResolve(0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Enemy" && !playerIsHit)
        {
            audioManager.Play("PlayerTakeDamage");
            EnemyStatus enemyStatus = other.gameObject.GetComponent<EnemyCombat>().enemyStatus;
            Rigidbody2D enemyRb = other.gameObject.GetComponent<Rigidbody2D>();
            enemyStatus.HitPlayer(playerStatus);
            StartCoroutine(flashWhite(flashDuration));
            //needs fixing - MOVE ENEMY BY TIME
            Vector3 direction3D = (transform.position - other.transform.position);
            Vector2 direction = new Vector2(direction3D.x, direction3D.y).normalized;
            enemyRb.AddForce(-direction * enemyStatus.knockBack, ForceMode2D.Force); //pushback tutorial, might help https://www.youtube.com/watch?v=sdGeGQPPW7E
        }

        if (other.gameObject.tag == "Hazard" && !playerIsHit)
        {
            audioManager.Play("PlayerTakeDamage");
            playerStatus.TakeDamage(30f);
            StartCoroutine(flashWhite(flashDuration));
            Vector3 direction3D = (transform.position - other.transform.position);
            Vector2 direction = new Vector2(direction3D.x, direction3D.y).normalized;
            rb.AddForce(direction * 1500f, ForceMode2D.Force);
        }
    }

    IEnumerator flashWhite(float duration)
    {
        playerIsHit = true;
        spriteRenderer.material = matWhite;

        yield return new WaitForSeconds(duration);
        spriteRenderer.material = matDefault;
        playerIsHit = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isSuperSlashing && other.gameObject.tag == "Enemy")
        {
            EnemyCombat enemy = other.gameObject.GetComponent<EnemyCombat>();
            enemy.TakeAnyDamage(enemy.enemyStatus.maxHealth);
        }
        if (isSuperSlashing && other.gameObject.tag == "ground")
        {
            isSuperSlashing = false;
            playerMovement.playerCollider.isTrigger = false;
            isReadyForSlash = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Poison"))
        {
            playerStatus.TakeDamage(Time.deltaTime * other.gameObject.GetComponent<Poison>().damage);
        }
    }

    private void heal()
    {
        audioManager.Play("PlayerHeal");
        playerStatus.FillHP();
        playerStatus.SetResolve(0f);
        GameObject healthParticles = Instantiate(healEffect, transform.position, Quaternion.identity);
        healthParticles.transform.parent = transform;
    }

    void changeSwordPosition(Vector3 direction)
    {
        Sword.transform.position = playerStatus.GetTransform().position;
        Sword.transform.position += direction * swordOffset;
    }

    void changeSwordRotation(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Sword.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Sword.GetComponent<Sword>().direction = new Vector2(direction.x, direction.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(superSlashDirection.x, superSlashDirection.y, 0));
    }
}
