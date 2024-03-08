using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Spell Ranks
public enum SpellRanks
{
    Novice,
    Intermediate,
    Advanced,
    Saint,
    King,
    Imperial,
    God,
}

// Spell Elements
public enum Elements
{
    Fire, Water, Earth, Wind
}

public enum SpellRankXP
{
    Novice = 5,
    Intermediate = 10,
    Advanced = 30,
    Saint = 100,
    King = 500,
    Imperial = 1000,
    God = 5000
}


public class PlayerStatsManager : MonoBehaviour
{
    private static PlayerStatsManager instance;
    
    [SerializeField] private Image healthBar, manaBar;
    [SerializeField] private TextMeshProUGUI healthNumbers, manaNumbers;
    
    private Dictionary<SpellRanks, int> RankRequirements = new Dictionary<SpellRanks, int>()
    {
        // Key is level requirement for rank promotion, value is spell rank
        { SpellRanks.Novice, 1 },
        { SpellRanks.Intermediate, 15 },
        { SpellRanks.Advanced, 30 },
        { SpellRanks.Saint, 60 },
        { SpellRanks.King, 120 },
        { SpellRanks.Imperial, 240 },
        { SpellRanks.God, 500 }
    };
    
    // Player stats
    private int healthLevel = 1;
    // Magic Ranks
    private SpellRanks fireRank = SpellRanks.Novice;
    private SpellRanks waterRank = SpellRanks.Novice;
    private SpellRanks earthRank = SpellRanks.Novice;
    private SpellRanks windRank = SpellRanks.Novice;

    public int fireLevel = 1;
    public int waterLevel = 1;
    public int earthLevel = 1;
    public int windLevel = 1;

    public void CheckRankStatus(Elements type, SpellRanks currentRank)
    {
        Debug.Log("Evaluating Rank");
        Debug.Log("Current Rank: " + fireRank);

        int levelValue = GetMagicTypeLevel(type);
        int levelRequirement = 0;
        SpellRanks newRank = SpellRanks.Novice;
        
        foreach (var key in RankRequirements.Keys) // loop through keys
        {
            if (currentRank == key)
            {
                newRank = key + 1;
                Debug.Log("Next Rank:" + newRank);
                levelRequirement = RankRequirements[newRank];
                Debug.Log("int level:" + levelRequirement);
            }
        }

        if (newRank == SpellRanks.Novice || levelRequirement == 0)
        {
            // Exit if no rank was found or levelRequirement was not set
            return;
        }
        
        
        if (levelValue == levelRequirement)
        {
            // promote rank if level req is met
            Debug.Log(type + " proficiency has increased to " + newRank);

            // Get what element's level 
            switch (type)
            {
                case Elements.Fire:
                    fireRank = newRank;
                    break;
                    
                case Elements.Water:
                    waterRank = newRank;
                    break;
                    
                case Elements.Earth:
                    earthRank = newRank;
                    break;
                    
                case Elements.Wind:
                    windRank = newRank;
                    break;
            }
        }
        else
        {
            Debug.Log("No promotion in " + type);
        }
    }

    public int GetMagicTypeLevel(Elements type)
    {
        switch (type)
        {
            case Elements.Fire:
                return fireLevel;
            
            case Elements.Water:
                return waterLevel;
            
            case Elements.Earth:
                return earthLevel;
            
            case Elements.Wind:
                return windLevel;
            
            default:
                Debug.Log("Error no elemental magic type found");
                return 0;
        }
    }

    public int GetSpellXP(SpellRanks rank)
    {
        Debug.Log("spell rank: " + rank);
        
        switch (rank)
        {
            case SpellRanks.Novice:
                Debug.Log("Getting XP: " + (int)SpellRankXP.Novice);
                return (int)SpellRankXP.Novice;
            
            case SpellRanks.Intermediate:
                Debug.Log("Getting XP: " + (int)SpellRankXP.Intermediate);
                return (int)SpellRankXP.Intermediate;
            
            case SpellRanks.Advanced:
                Debug.Log("Getting XP: " + (int)SpellRankXP.Advanced);
                return (int)SpellRankXP.Advanced;
            
            case SpellRanks.Saint:
                Debug.Log("Getting XP: " + (int)SpellRankXP.Saint);
                return (int)SpellRankXP.Saint;
            
            case SpellRanks.King:
                Debug.Log("Getting XP: " + (int)SpellRankXP.King);
                return (int)SpellRankXP.King;
            
            case SpellRanks.Imperial:
                Debug.Log("Getting XP: " + (int)SpellRankXP.Imperial);
                return (int)SpellRankXP.Imperial;
            
            case SpellRanks.God:
                Debug.Log("Getting XP: " + (int)SpellRankXP.God);
                return (int)SpellRankXP.God;
            
            default:
                Debug.Log("Getting XP: 0");
                return 0;
        }
    }
    
    // Current values
    private float currentHealth, maxHealth, currentMana, maxMana;

    private PlayerStatsManager()
    {
        instance = this;
    }

    public static PlayerStatsManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerStatsManager();
            }

            return instance;
        }
    }

    private void Start()
    {
        // Set levels
        healthLevel = 1;

        // Set player values
        maxHealth = 10 + (healthLevel * 10);
        currentHealth = maxHealth;
        maxMana = 20;
        //maxMana = Random.Range(20, 100);
        currentMana = maxMana;

        // Set health and mana values for text
        healthNumbers.text = currentHealth.ToString() + " / " + maxHealth;
        manaNumbers.text = currentMana.ToString() + " / " + maxMana;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        healthNumbers.text = currentHealth.ToString() + " / " + maxHealth;
        
        healthBar.fillAmount = currentHealth / maxHealth;
    }
    
    public void UseMana(float manaCost)
    {
        if (currentMana - manaCost < 0)
        {
            return;
        }
        currentMana -= manaCost;
        
        manaNumbers.text = currentMana.ToString() + " / " + maxMana;
        
        manaBar.fillAmount = currentMana / maxMana;
    }

    public float GetCurrentMana()
    {
        return currentMana;
    }

    public void Heal(float healingAmount)
    {
        currentHealth += healingAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, 100);
        
        healthNumbers.text = currentHealth.ToString() + " / " + maxHealth;

        healthBar.fillAmount = currentHealth / maxHealth;
    }
    
    public void RestoreMana(float amount)
    {
        currentMana += amount;
        //currentMana = Mathf.Clamp(currentMana, 0, 100);
        
        manaNumbers.text = currentMana.ToString() + " / " + maxMana;

        manaBar.fillAmount = currentMana / maxMana;
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
