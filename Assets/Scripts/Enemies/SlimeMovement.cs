using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    private Transform player;
    private float maxViewingDistance = 10f;
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
        if (player == null) return;

        Vector2 moveDir = player.position - transform.position;
        bool isInRange = Math.Abs(moveDir.magnitude) < maxViewingDistance;
        int mask = LayerMask.GetMask("Ground", "Items", "Player");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDir.normalized * new Vector2(1, 0), maxViewingDistance, mask);
        if (isInRange && hit && hit.collider.CompareTag("Player"))
        {
            if (!isJumping)
            {
                StartCoroutine(MoveToPlayer(moveDir.normalized));
            }
        }
    }

    IEnumerator MoveToPlayer(Vector2 moveDir)
    {
        isJumping = true;
        rb.AddForce(new Vector2(moveDir.x * jumpWidth, jumpHeight), ForceMode2D.Impulse);
        yield return new WaitForSeconds(1f);
        isJumping = false;
    }
}
