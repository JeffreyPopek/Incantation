using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class MagicManager : MonoBehaviour
{
    // Cast point for the player
    [SerializeField] private Transform castPoint;


    [SerializeField] private SpeechRecognitionTest speech;

    [SerializeField] private GameObject[] spellBook;

    private static MagicManager instance;

    private string gm_LastSaid = "";

    private GameObject spellToCast;

    // Spellbook
    private Dictionary<string, string> SpellBook = new Dictionary<string, string>()
    {
        // Key is the incantation, value is the spell name
        {"Firebolt", "Firebolt"},
        {"O raging fire, offer us a great and blazing gift. Fireball", "Fireball"},
        {"I call a refreshing burbling stream here and now. Water Ball", "Waterball"}
    };
    


 
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

    public void CastSpell(string incantation)
    {
        GetSpellFromIncantation(incantation);

        if (PlayerStatsUIManager.Instance.GetCurrentMana() >= spellToCast.GetComponent<BaseSpell>().manaCost)
        {
            PlayerStatsUIManager.Instance.UseMana(GetManaCost(spellToCast.GetComponent<BaseSpell>()));
            Instantiate(spellToCast, castPoint.position, castPoint.rotation);
        }
        else
        {
            Debug.Log("YOU HAVE NO MANA");
        }
        
    }

    public float GetManaCost(BaseSpell spell)
    {
        return spell.manaCost;
    }
    
    public float CalculateDamage(BaseSpell spell, int playerIntelligenceLevel)
    {
        /*
        damage = attack * attack / defense
        attack = spell base damage + (int level / 2)
        */
        
        float totalDamage = 0;

       totalDamage = spell.baseDamage + playerIntelligenceLevel;
        
        // add enemy defence later
        //        totalDamage = ((spellbaseDamage + (playerIntelligenceLevel / 2)) * 2) / enemyDefence;

        return totalDamage;
    }


    public string GetLastSaid()
    {
        return gm_LastSaid;
    }
    
    // Getting spell from voice
    
    public void GetSpellFromIncantation(string incantation)
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
        int incantationTolerance = 30;
        if (smallestDistance > incantationTolerance)
        {
            // Tell player incantation has failed
            //Debug.Log("No matching incantation");
            //return;
        }
        Debug.Log("Casting: " + SpellBook[keyToGet]);

        // Get spell prefab
        spellToCast = GetSpellGameObject(SpellBook[keyToGet]);
    }

    private GameObject GetSpellGameObject(string spellName)
    {
        foreach (var spell in spellBook)
        {
            if (spell.name == spellName)
            {
                //Debug.Log(spell.name);
                return spell;
            }
        }
        
        Debug.Log("No Spell found in GetSpellGameObject()");
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
