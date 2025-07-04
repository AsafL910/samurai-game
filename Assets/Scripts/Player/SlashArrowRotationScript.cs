using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashArrowRotationScript : MonoBehaviour
{
    private Vector3 direction;
    public Transform player;

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
    }
    void Update()
    {
        if (GetComponent<SpriteRenderer>().enabled)
        {
            transform.position = player.position;
            direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
         }
    }

    public Vector3 GetDirection() {
        return direction;
    }

}
