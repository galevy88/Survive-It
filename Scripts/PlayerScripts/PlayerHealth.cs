using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float health = 100f;
    // GUI
    [SerializeField] TextMeshProUGUI healthText;

    // show health information every single frame
    void Update()
    {
        DisplayText();
    }

    // display health on GUI
    private void DisplayText()
    {
        float currentHealth = health;
        healthText.text = currentHealth.ToString();
    }

    // decrease hp when zombie attack
    public void TakeDamage(float damage)
    {
        health -= damage;
        // if player die show the menu by calling to appropriate method in DeathHandler
        if(health <= 0)
        {
            health = 0;
            GetComponent<DeathHandler>().HandleDeath();
        }
    }

    // add health when take heart
    public void AddHealth(float add)
    {
        health += add;
    }
}
