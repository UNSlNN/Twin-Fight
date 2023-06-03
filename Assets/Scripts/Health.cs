using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider healthbar;
    public float currentHealth;
    private float maxHealth = 100f;
    public bool islose;
    void Start()
    {
        // Set health value to slider 
        currentHealth = maxHealth;
        healthbar.minValue = 0f;
        healthbar.maxValue = maxHealth;
        healthbar.value = currentHealth;
    }

    public void DamageAmount(float damage)  // Calculate Damage
    {
        currentHealth -= damage;
        if (currentHealth <= 0f)
        {
            islose = true;
        }
        healthbar.value = currentHealth;
    }
}
