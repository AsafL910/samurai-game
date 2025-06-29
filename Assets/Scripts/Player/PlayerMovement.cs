using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator playerAnimationController;
    public LayerMask groundLayer;
    public Rigidbody2D rb;
    public Transform player;
    public PlayerStatus playerStatus;
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
    }

    void Update()
    {
        HorizontalMove();
        FlipCharacter();

        if (!isGrounded)
        {
            isGrounded = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Abs(transform.localScale.x * raySizeMultiplier), groundLayer);
            playerAnimationController.SetBool("isGrounded", isGrounded);
            if (isGrounded)
            {
                audioManager.Play("DirtLanding");
            }
        }
        else
        {
            isGrounded = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Abs(transform.localScale.x * raySizeMultiplier), groundLayer);
            playerAnimationController.SetBool("isGrounded", isGrounded);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                Jump();
                canDoubleJump = true;
            }
            else if (canDoubleJump && playerStatus.CanDoubleJump())
            {
                Jump();
                canDoubleJump = false;
            }
            
        }

        JumpPhysicsFix();

        //ColliderFix();
    }

	private void FixedUpdate()
	{
        if (!gameObject.GetComponent<CapsuleCollider2D>().isTrigger) {
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
        rb.AddForce(Vector2.up*jumpVelocity,ForceMode2D.Impulse);
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

        playerStatus.SetTransform(player.transform);
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
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * transform.localScale.x*raySizeMultiplier);
    }
}
