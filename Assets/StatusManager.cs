using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatusManager : MonoBehaviour
{
    public HealthBar healthBar;
    public bool isPlayer;
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
        if(amt < 0)
        {
            if (!isPlayer)
            {
                FindObjectOfType<AudioManager>().Play("hitboss");
            }
        } else if (amt > 0)
        {
            FindObjectOfType<AudioManager>().Play("heal");
        }
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
        if(!isPlayer)
        {
            GameStateManager.ChangeState(GameStateManager.GameState.WinState);
        }
        else
        {
            GameStateManager.ChangeState(GameStateManager.GameState.LoseState);
        }
    }
    public void SetMaxHealth(int newHealth)
    {
        maxHealth = newHealth;
        healthBar.SetMaxHealth(newHealth);
    }

}
