using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjAccelerate : MonoBehaviour
{
    public float horizSpeed;
    public float vertSpeed;
    public float accelAmount;

    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(horizSpeed, vertSpeed);
    }
    private void FixedUpdate()
    {
        Vector2 currspeed = rb.velocity;
        currspeed *= accelAmount;
        rb.velocity = currspeed;
    }
}
