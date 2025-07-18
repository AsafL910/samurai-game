using System.Collections;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3.5f;
    public float waitTime = 2f;
    public float detectionRange = 20f;
    public Transform player;

    private int currentPointIndex = 0;
    private float waitTimer = 0f;
    private bool isWaiting = false;
    private bool isChasing = false;

    private Rigidbody2D rb;
    private Vector2 direction;

    private Animator animator;
    public GameObject ShurikenPrefab;
    public float throwSpeed = 100f;

    private bool isThrowingShuriken = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = PlayerMovement.instance.transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rb.velocity.normalized, detectionRange, LayerMask.GetMask("Player", "Ground"));
        if (hit.collider != null && hit.collider.CompareTag("Player") && (distanceToPlayer <= detectionRange))
        {
            isChasing = true;
        }
        else if (isChasing && distanceToPlayer > detectionRange)
        {
            isChasing = false;
        }

        //add throwing shurikens if chasing player every 2 seconds

        if (isChasing)
        {
            if (!isThrowingShuriken)
            {
                StartCoroutine(ThrowShuriken(0.5f));
            }
            ChasePlayer();
        }
        else
        {
            Patrol();
        }

        // Flip the sprite to face movement direction
        if (direction.x != 0)
        {
            Vector3 scale = transform.localScale;
            if (direction.x > 0)
                scale.x = Mathf.Abs(transform.localScale.x);
            else
                scale.x = -Mathf.Abs(transform.localScale.x);

            transform.localScale = scale;
        }
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        Transform targetPoint = patrolPoints[currentPointIndex];
        direction = (targetPoint.position - transform.position).normalized;

        if (!isWaiting)
        {
            rb.velocity = new Vector2(direction.x * patrolSpeed, rb.velocity.y);

            if (Vector2.Distance(transform.position, targetPoint.position) < 0.2f)
            {
                rb.velocity = Vector2.zero;
                isWaiting = true;
                waitTimer = waitTime;
            }
        }
        else
        {
            animator.SetFloat("PlayerHorizontalSpeed", 0);
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0f)
            {
                currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
                isWaiting = false;
                animator.SetFloat("PlayerHorizontalSpeed", 1);

            }
        }
    }

    IEnumerator ThrowShuriken(float duration)
    {
        isThrowingShuriken = true;
        yield return new WaitForSeconds(duration);
        var shuriken = Instantiate(ShurikenPrefab);
        //position the shuriken in front of the enemy
        shuriken.tag = "EnemyShuriken";
        shuriken.gameObject.transform.position = transform.position + new Vector3(2.5f * transform.localScale.normalized.x, 0, 0);
        shuriken.gameObject.GetComponent<Rigidbody2D>().AddForce((player.position - transform.position).normalized * throwSpeed, ForceMode2D.Impulse);
        isThrowingShuriken = false;
    }

    void ChasePlayer()
    {
        direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y);
    }
}
