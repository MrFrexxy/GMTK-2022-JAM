using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjJagged : MonoBehaviour
{
    public float vertSpeed;
    public float horizSpeed;

	Vector2 speed = new Vector2(0,0);
	public int i = 0;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(horizSpeed, vertSpeed);
		speed.x = horizSpeed;
		speed.y = vertSpeed;


    }

	void FixedUpdate() 
	{
		i++;
		if( i % 40 == 20 || i % 40 == 30  ){
			speed.y *= -1;
		}

		rb.velocity = speed;

	}

}
