using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjCircle : MonoBehaviour
{
    public float projSpeed;
    private Rigidbody2D rb;
    public Vector2 rotateCenter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 axis = new Vector3(0, 0, 1);
        transform.RotateAround(rotateCenter, axis, projSpeed * Time.deltaTime);
    }
}
