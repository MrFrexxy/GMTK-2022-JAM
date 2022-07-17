using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjSinWave : MonoBehaviour
{

	public float projSpeed;
    private Rigidbody2D rb;
    

    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		rb.velocity = (new Vector2(Mathf.Sin(transform.position.y) , 1) )*projSpeed;
        
    }
}
