using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjDownZigzag : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public int turnaround;
    private int tick;
    // Start is called before the first frame update
    void Start()
    {
        tick = 0;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed, -speed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tick++;
        if(tick % turnaround == 0)
        {
            Vector2 currspeed = rb.velocity;
            currspeed.x *= -1;
            rb.velocity = currspeed;
        }
    }
}
