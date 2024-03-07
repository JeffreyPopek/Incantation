using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatsManager : MonoBehaviour
{
    private static PlayerStatsManager instance;
    
    [SerializeField] private Image healthBar, manaBar;
    [SerializeField] private TextMeshProUGUI healthNumbers, manaNumbers;
    
    // not enough mana (temp)
    //[SerializeField] private TextMeshProUGUI noManaText;

    
    // Player Stats
    private int healthLevel, intelligenceLevel;
    
    // Spell Ranks
    enum SpellRanks
    {
        Novice = 1,
        Intermediate = 2,
        Advanced = 3,
        Saint = 4,
        King = 5,
        Imperial = 6,
        God = 7,
    }
    
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
    
    // Magic Ranks
    private SpellRanks fireRank = SpellRanks.Novice;
    private SpellRanks waterRank = SpellRanks.Novice;
    private SpellRanks earthRank = SpellRanks.Novice;
    private SpellRanks windRank = SpellRanks.Novice;

    private int fireLevel = 15;
    private int waterLevel = 1;
    private int earthLevel = 1;
    private int windLevel = 1;

    private void CheckRankStatus(int currentLevel, SpellRanks currentRank)
    {
        Debug.Log("Evaluating Rank");
        Debug.Log("Current Rank: " + fireRank);

        int levelValue = 0;
        SpellRanks newRank = SpellRanks.Novice;
        
        foreach (var key in RankRequirements.Keys) // loop through keys
        {
            if (currentRank == key)
            {
                levelValue = RankRequirements[key];
                newRank = key + 1;
                //break;
            }
        }

        if (newRank == SpellRanks.Novice)
        {
            // Exit if no rank was found somehow
            return;
        }
        
        if (currentLevel >= levelValue)
        {
            fireRank = newRank;
        }
        else
        {
            Debug.Log("No promotion");
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
        intelligenceLevel = 1;
        
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
        // TEMP
        if (Input.GetKeyDown(KeyCode.L))
        {
            CheckRankStatus(fireLevel, fireRank);
            
            Debug.Log("Fire rank: " + fireRank);
        }
        
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
