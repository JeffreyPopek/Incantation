using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerStatsUIManager : MonoBehaviour
{
    [SerializeField] private Image healthBar, manaBar;
    [SerializeField] private TextMeshProUGUI healthNumbers, manaNumbers;

    private float currentHealth, currentMana, maxHealth, maxMana;
    

    private void Start()
    {
        // Set health and mana values in bar image
        currentHealth = PlayerStatsManager.Instance.GetCurrentHealth();
        currentMana = PlayerStatsManager.Instance.GetCurrentMana();
        maxHealth = currentHealth;
        maxMana = currentMana;

        // Set health and mana values for text
        healthNumbers.text = currentHealth.ToString() + " / " + maxHealth;
        manaNumbers.text = currentMana.ToString() + " / " + maxMana;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        healthNumbers.text = currentHealth.ToString() + " / " + maxHealth;
        manaNumbers.text = currentMana.ToString() + " / " + maxMana;
        
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void Heal(float healingAmount)
    {
        currentHealth += healingAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, 100);
        
        healthNumbers.text = currentHealth.ToString() + " / " + maxHealth;
        manaNumbers.text = currentMana.ToString() + " / " + maxMana;

        healthBar.fillAmount = currentHealth / maxHealth;
    }
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.visible)
            {
                Cursor.visible = false;
            }
            else
            {
                Cursor.visible = true;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(1);
        }
        
        if (Input.GetKeyDown(KeyCode.O))
        {
            Heal(1);
        }
    }
}
