using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int projectileDamage;
    // player object
    private GameObject player;
    private void Start()
    {
        player = FindObjectOfType<MovementController>().gameObject;
        StartCoroutine(DestroyObject());
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().Play("hit");
            player.GetComponent<StatusManager>().AddHealth(projectileDamage * -1);
            Destroy(this.gameObject);
        }
        if(col.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(4);
        Destroy(this.gameObject);
    }
}
