using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    private Transform player;
    public float maxViewingDistance;
    public Rigidbody2D rb;
    public float jumpHeight;
    public float jumpWidth;
    private bool isJumping;
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject.transform;
        isJumping = false;
    }

    void Update()
    {
        Vector2 moveDir = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
        if (Mathf.Abs(moveDir.magnitude) < maxViewingDistance && !isJumping) {
            StartCoroutine(MoveToPlayer(moveDir.normalized));
        }
    }

    IEnumerator MoveToPlayer(Vector2 moveDir) {
        isJumping = true;
        rb.AddForce(new Vector2(moveDir.x * jumpWidth, jumpHeight), ForceMode2D.Impulse);
        yield return new WaitForSeconds(1f);
        isJumping = false;
    }

	private void OnDrawGizmosSelected()
	{
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, maxViewingDistance);
    }
}
