using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public AttackPattern attackPattern;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DoAttack()
    {
        for(int i = 0; i < attackPattern.projectiles.Length; i++) 
        {
            StartCoroutine(SpawnProjectile(attackPattern.projectiles[i]));
        }
    }

    IEnumerator SpawnProjectile(AttackPattern.ProjectileSpawn proj)
    {
        yield return new WaitForSeconds(proj.delay);
        Instantiate(proj.bulletType, proj.spawnPos, transform.rotation);
    }
}
