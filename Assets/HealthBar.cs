using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI tmp;
    private int maxHealth = 0;
    private int health = 0;
    public void SetHealth(int health)
    {
        this.health = health;
        slider.value = health;
        UpdateText();
    }
    
    public void SetMaxHealth(int health)
    {
        maxHealth = health;
        this.health = health;
        slider.maxValue = health;
        slider.value = health;
        UpdateText();
    }
    private void UpdateText()
    {
        if (tmp)
        {
            tmp.SetText(health + "/" + maxHealth);
        }
    }
}
