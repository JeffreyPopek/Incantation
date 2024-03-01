using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicManager : MonoBehaviour
{
    [SerializeField] private SpeechRecognitionTest speech;
    [SerializeField] private GameObject _player;
    
    private static MagicManager instance;

    private string gm_LastSaid = "";
    
    struct SpellData
    {
        private string spellName;
        private int manaCost;
    }
    
    // Spellbook
    private Dictionary<string, GameObject> manaCosts = new Dictionary<string, GameObject>()
    {
        // {"Firebolt", },
        // {"Fireball", }
    };
    
    // Spellbook
    private Dictionary<string, string> SpellBook = new Dictionary<string, string>()
    {
        // Key is the incantation, value is the spell name
        {"I cast Firebolt", "Firebolt"},
        {"I cast fireball", "fireball"},
        {"I cast waterball", "Waterball"}
    };
    
    //Player stats
    private int gm_HealthLevel, gm_IntelligenceLevel;

 
    private MagicManager() {
        // initialize your game manager here. Do not reference to GameObjects here (i.e. GameObject.Find etc.)
        // because the game manager will be created before the objects
        instance = this;
    }    
 
    public static MagicManager Instance 
    {
        get {
            if(instance==null) 
            {
                instance = new MagicManager();
            }
 
            return instance;
        }
    }


    private void Awake()
    {
        _player.GetComponent<PlayerMagic>();
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
        // foreach (KeyValuePair<string, int> item in SpellBook)
        // {
        //     if (item.Key == spellName)
        //     {
        //         return item.Value;
        //     }
        // }

        // If nothing is found then return 0
        return 0;
    }
    
    
    public string GetLastSaid()
    {
        return gm_LastSaid;
    }
    
    // Getting spell from voice

    public string GetSpellFromIncantation(string incantation)
    {
        int smallestDistance = 0;
        int prevSmallestDistance = 99999999;
        string keyToGet = "";
        
        foreach (var key in SpellBook.Keys) // loop through keys
        {
            smallestDistance = CalculateLevenshteinDistance(key, incantation);
            if (smallestDistance < prevSmallestDistance)
            {
                prevSmallestDistance = smallestDistance;
                keyToGet = key;
            }
        }
        
        // If said incantation is too off from the original then don't do anything
        int incantationTolerance = 100;
        if (smallestDistance > incantationTolerance)
        {
            // Tell player incantation has failed
            return null;
        }
        Debug.Log(SpellBook[keyToGet]);
        
        _player.GetComponent<PlayerMagic>().CastSpell();
        return SpellBook[keyToGet];
    }

    public GameObject GetSpellObject(string spell)
    {

        return null;
    }
    
    
    // The lower the number the closer the two strings are to matching
    public static int CalculateLevenshteinDistance(string source1, string source2)
    {
        var source1Length = source1.Length;
        var source2Length = source2.Length;

        var matrix = new int[source1Length + 1, source2Length + 1];

        // First calculation, if one entry is empty return full length
        if (source1Length == 0)
            return source2Length;

        if (source2Length == 0)
            return source1Length;

        // Initialization of matrix with row size source1Length and columns size source2Length
        for (var i = 0; i <= source1Length; matrix[i, 0] = i++){}
        for (var j = 0; j <= source2Length; matrix[0, j] = j++){}

        // Calculate rows and collumns distances
        for (var i = 1; i <= source1Length; i++)
        {
            for (var j = 1; j <= source2Length; j++)
            {
                var cost = (source2[j - 1] == source1[i - 1]) ? 0 : 1;

                matrix[i, j] = Mathf.Min(
                    Mathf.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
                    matrix[i - 1, j - 1] + cost);
            }
        }
        // return result
        //Debug.Log(matrix[source1Length, source2Length]);
        return matrix[source1Length, source2Length];
    }
}
