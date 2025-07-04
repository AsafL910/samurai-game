using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float spinSpeed = 10f;
    public Rigidbody2D rb;
    void Start()
    {
        rb.AddTorque(spinSpeed, ForceMode2D.Impulse);
    }

    void Update()
    {

    }
}
