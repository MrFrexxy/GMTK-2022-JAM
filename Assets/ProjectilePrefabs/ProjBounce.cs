using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjBounce : MonoBehaviour
{
    public float horizSpeed;
    public float vertSpeed;
    private Rigidbody2D rb;
    public float turnaroundFactor;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(horizSpeed, vertSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 newSpeed = rb.velocity;
        newSpeed.x += (horizSpeed * -1 * turnaroundFactor);
        newSpeed.y += (vertSpeed * -1 * turnaroundFactor);
        rb.velocity = newSpeed;
    }
}
