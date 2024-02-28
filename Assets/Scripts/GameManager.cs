using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private static GameManager instance;

    private string gm_LastSaid = "";
    
    // Spellbook
    private Dictionary<string, int> SpellBook = new Dictionary<string, int>()
    {
        {"Firebolt", 5},
        {"Fireball", 25}
    };
    
    // Game states
    private bool gm_GameStarted, gm_GamePaused;
    //Player stats
    private int gm_HealthLevel, gm_IntelligenceLevel;

 
    private GameManager() {
        // initialize your game manager here. Do not reference to GameObjects here (i.e. GameObject.Find etc.)
        // because the game manager will be created before the objects
    }    
 
    public static GameManager Instance 
    {
        get {
            if(instance==null) 
            {
                instance = new GameManager();
            }
 
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }
    
    private float CalculateDamage(string spellName, int playerIntelligenceLevel, int enemyDefence)
    {
        /*
        damage = attack * attack / defense
        attack = spell base damage + (int level / 2)
        */
        
        float totalDamage = 0;

        int spellbaseDamage = GetSpellBaseDamage(spellName);
        totalDamage = ((spellbaseDamage + (playerIntelligenceLevel / 2)) * 2) / enemyDefence;
        
        return totalDamage;
    }

    int GetSpellBaseDamage(string spellName)
    {
        // Find spell base damage based on spell name
        foreach (KeyValuePair<string, int> item in SpellBook)
        {
            if (item.Key == spellName)
            {
                return item.Value;
            }
        }

        // If nothing is found then return 0
        return 0;
    }

    public void SetLastSaid(string lastSaid)
    {
        gm_LastSaid = lastSaid;
        Debug.Log(gm_LastSaid);
    }
    
    public string GetLastSaid()
    {
        return gm_LastSaid;
    }

}
