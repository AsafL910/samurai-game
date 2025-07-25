using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashArrowRotationScript : MonoBehaviour
{
    private Vector3 direction;
    public Transform player;
    public Camera myCamera;
    public static SlashArrowRotationScript instance;
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
        GetComponent<SpriteRenderer>().enabled = false;
        player = FindObjectOfType<PlayerMovement>().transform;
        myCamera = Camera.main;
    }
    void Update()
    {
        if (player == null)
        {
            PlayerMovement foundPlayer = FindObjectOfType<PlayerMovement>();
            if (foundPlayer != null)
            {
                player = foundPlayer.transform;
            }
            else
            {
                return; // No player yet, skip update
            }
        }
        transform.position = player.position;
        direction = Input.mousePosition - myCamera.WorldToScreenPoint(player.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public Vector3 GetDirection()
    {
        return direction;
    }
}
