using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [HideInInspector]
    public float vertInput;
    [HideInInspector]
    public float horizInput;

    public float moveSpeed;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizInput * moveSpeed, vertInput * moveSpeed);
    }

    void GetInput()
    {
        horizInput = Input.GetAxisRaw("Horizontal");
        vertInput = Input.GetAxisRaw("Vertical");
    }
}
