using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjStraight : MonoBehaviour
{
    public float vertSpeed;
    public float horizSpeed;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(horizSpeed, vertSpeed);
    }

}
