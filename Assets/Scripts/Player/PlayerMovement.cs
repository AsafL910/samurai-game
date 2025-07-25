using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator playerAnimationController;
    public LayerMask groundLayer;
    public Rigidbody2D rb;
    public Transform player;
    public bool canDoubleJump;
    public Vector3 moveDir;
    public CapsuleCollider2D playerCollider;
    private AudioManager audioManager;

    public bool isGrounded;
    public float raySizeMultiplier = 2f;
    public float fallMultiplier;
    public float jumpMultiplier;

    float gravity;

    public float jumpVelocity = 15f;

    public float horizontalMove;

    public float playerSpeed = 5f;

    public float rayLength = 0.4f;
    public float raySpacingFactor = 1.3f;

    public static PlayerMovement instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        transform.position = FindObjectOfType<GameManager>().checkpoint;
        gravity = rb.gravityScale;
        Debug.Log("PlayerMovement started. Player position: " + transform.position + ", checkpoint: " + FindObjectOfType<GameManager>().checkpoint);
        ScoreManager.UpdateTotalScore("Forest");

    }

    void Update()
    {
        HorizontalMove();
        FlipCharacter();

        if (!isGrounded)
        {
            isGrounded = IsGrounded();
            playerAnimationController.SetBool("isGrounded", isGrounded);
            if (isGrounded)
            {
                audioManager.Play("DirtLanding");
            }
        }
        else
        {
            isGrounded = IsGrounded();
            playerAnimationController.SetBool("isGrounded", isGrounded);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                Jump();
                canDoubleJump = true;
            }
            else if (canDoubleJump && PlayerState.GetPlayerStatus().CanDoubleJump())
            {
                Jump();
                canDoubleJump = false;
            }

        }

        JumpPhysicsFix();

        //ColliderFix();
    }

    bool IsGrounded()
    {
        var capsule = GetComponent<CapsuleCollider2D>();
        Vector2 boundsCenter = capsule.bounds.center;
        float width = capsule.bounds.extents.x * raySpacingFactor;
        float y = capsule.bounds.min.y;

        Vector2 left = new Vector2(boundsCenter.x - width, y);
        Vector2 center = new Vector2(boundsCenter.x, y);
        Vector2 right = new Vector2(boundsCenter.x + width, y);

        bool grounded =
            Physics2D.Raycast(left, Vector2.down, rayLength, groundLayer) ||
            Physics2D.Raycast(center, Vector2.down, rayLength, groundLayer) ||
            Physics2D.Raycast(right, Vector2.down, rayLength, groundLayer);
        // Optional: debug visualization
        Debug.DrawRay(left, Vector2.down * rayLength, Color.red);
        Debug.DrawRay(center, Vector2.down * rayLength, Color.red);
        Debug.DrawRay(right, Vector2.down * rayLength, Color.red);
        return grounded;
    }

    private void FixedUpdate()
    {
        if (!gameObject.GetComponent<CapsuleCollider2D>().isTrigger)
        {
            rb.velocity = new Vector2(moveDir.x * playerSpeed * Time.fixedDeltaTime, rb.velocity.y);
        }
    }

    public void JumpPhysicsFix()
    {
        playerAnimationController.SetFloat("PlayerVerticalSpeed", rb.velocity.y);

        if (rb.velocity.y < 0f)
        {
            rb.gravityScale = gravity * fallMultiplier;

        }
        else
        {
            if (Input.GetKey(KeyCode.Space))
            {
                rb.gravityScale = gravity * jumpMultiplier / 3.0f;
            }
            else
            {
                rb.gravityScale = gravity * jumpMultiplier;
            }
        }
    }

    public void Jump()
    {
        audioManager.Play("PlayerJump");
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
    }

    void HorizontalMove()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        playerAnimationController.SetFloat("PlayerHorizontalSpeed", Mathf.Abs(horizontalMove));
        moveDir = new Vector3(horizontalMove, 0f).normalized;
        if (horizontalMove != 0 && isGrounded)
        {
            if (!audioManager.isPlaying("footsteps"))
            {
                audioManager.Play("footsteps");
            }
        }
        else
        {
            if (audioManager.isPlaying("footsteps"))
            {
                audioManager.Stop("footsteps");
            }
        }

        PlayerState.GetPlayerStatus().SetTransform(player.transform.position);
    }

    public void FlipCharacter()
    {
        Vector3 rotation = transform.localScale;

        if (horizontalMove < 0f)
        {
            rotation.x = -Mathf.Abs(rotation.x);
        }
        if (horizontalMove > 0f)
        {
            rotation.x = Mathf.Abs(rotation.x);
        }

        transform.localScale = rotation;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * transform.localScale.x * raySizeMultiplier);
    }
}
