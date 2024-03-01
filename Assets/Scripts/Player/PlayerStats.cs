using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    
    private int healthLevel, intelligenceLevel;
    private float currentHP, maxHP, currentMana, maxMana, currentXP, maxXP;
    
    // Start is called before the first frame update
    void Start()
    {
        // Set levels
        healthLevel = 1;
        intelligenceLevel = 1;
        
        // Set player values
        maxHP = 10 + (healthLevel * 10);
        maxMana = Random.Range(20, 100);
        currentMana = maxMana;
        
        Debug.Log("Mana:" + currentMana);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetPlayerStats()
    {
        // Get player stats from character creation
    }

    private void LevelUp()
    {
        
    }


}
