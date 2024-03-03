using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    private static PlayerStatsManager instance;
    [SerializeField] private PlayerStats _player;

    // Player Stats
    private int healthLevel, intelligenceLevel;
    private float currentHP, maxHP, currentMana, maxMana, currentXP, maxXP;
    private PlayerStatsManager() {
        // initialize your game manager here. Do not reference to GameObjects here (i.e. GameObject.Find etc.)
        // because the game manager will be created before the objects
        instance = this;
    }    
 
    public static PlayerStatsManager Instance 
    {
        get {
            if(instance==null) 
            {
                instance = new PlayerStatsManager();
            }
 
            return instance;
        }
    }


    public void Awake()
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
    }

    private void Start()
    {

    }
    
    public float GetCurrentHealth()
    {
        return currentHP;
    }
    
    public float GetCurrentMana()
    {
        return currentMana;
    }
}
