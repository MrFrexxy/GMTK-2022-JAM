using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjShootAtPlayer : MonoBehaviour
{
    public float projSpeed;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // get the location of the player
        Vector2 playerPos = FindObjectOfType<MovementController>().transform.position;
        // get location of projectile
        Vector2 projPos = transform.position;
        Vector2 projVelo = new Vector2((playerPos.x - projPos.x),
            (playerPos.y - projPos.y));
        rb.velocity = projVelo.normalized * projSpeed;
    }

}
