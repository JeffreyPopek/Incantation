using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    
    private int healthLevel, intelligenceLevel;
    private float currentHP, maxHP, currentMana, maxMana, currentXP, maxXP;
    
    private void Start()
    {
        // Set levels
        healthLevel = 1;
        intelligenceLevel = 1;
        
        // Set player values
        maxHP = 10 + (healthLevel * 10);
        currentHP = maxHP;
        maxMana = 20;
        //maxMana = Random.Range(20, 100);
        currentMana = maxMana;
        
        Debug.Log("Mana:" + currentMana);
    }
    




}
