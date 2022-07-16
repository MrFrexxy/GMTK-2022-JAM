using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private int currentHealth;
    private int currentShield;
    void Start()
    {
        currentHealth = maxHealth;
    }
    public void AddHealth(int amt)
    {
        currentHealth += amt;
        //clamp health
        currentHealth = currentHealth > maxHealth ? maxHealth : currentHealth;
        if(currentHealth < 0)
        {
            Die();
        }
    }
    public void AddBlock(int amt)
    {
        currentShield += amt;
        //so shield doesnt become negative
        currentShield = currentShield < 0 ? 0 : currentShield;
    }
    public void Die()
    {
        //do death stuff
        Destroy(gameObject);
    }
    public void SetMaxHealth(int newHealth)
    {
        maxHealth = newHealth;
    }
}
