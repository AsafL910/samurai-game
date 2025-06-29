using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float jumpForce;
    public Animator trampAnimatoionController;

	private void OnCollisionEnter2D(Collision2D other)
	{
        if (other.gameObject.tag == "Player" && other.transform.position.y > transform.position.y) {
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            other.gameObject.GetComponent<PlayerMovement>().canDoubleJump = true;
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            StartCoroutine(bounce());
        }
	}

    IEnumerator bounce() {
        trampAnimatoionController.SetBool("isPressed", true);
        FindObjectOfType<AudioManager>().Play("Trampoline");
        yield return new WaitForSeconds(trampAnimatoionController.GetCurrentAnimatorStateInfo(0).length/2);
        trampAnimatoionController.SetBool("isPressed", false);
    }
}
