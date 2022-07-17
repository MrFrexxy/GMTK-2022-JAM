using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatusManager : MonoBehaviour
{
    public HealthBar healthBar;
    private Animator animator;

    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private int currentHealth;
    private int currentShield;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        if (GetComponent<Animator>()) animator = GetComponent<Animator>();
    }
    public void AddHealth(int amt)
    {
        currentHealth += amt;
        //clamp health
        currentHealth = currentHealth > maxHealth ? maxHealth : currentHealth;
        healthBar.SetHealth(currentHealth);

        //play animation if health is negative
        if(amt < 0)
        {
            if (animator)
            {
                animator.SetTrigger("ishit");
            }
        }

        if(currentHealth <= 0)
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
        PlayerInfo.stageNumber++;
        SceneManager.LoadScene(1);
        Destroy(gameObject);
    }
    public void SetMaxHealth(int newHealth)
    {
        maxHealth = newHealth;
        healthBar.SetMaxHealth(newHealth);
    }

}
