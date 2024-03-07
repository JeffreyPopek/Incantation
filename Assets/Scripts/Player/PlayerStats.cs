using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStats : MonoBehaviour
{
    struct Ranks
    {
        // 1-7, 1 is novice 7 is god
        private int fireRank;
        private int waterRank;
        private int earthRank;
        private int windRank;
    }
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
