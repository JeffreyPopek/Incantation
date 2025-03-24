using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Vector3 = UnityEngine.Vector3;

public class MagicManager : MonoBehaviour
{
    // Cast point for the player
    [SerializeField] private Transform castPoint;
    [SerializeField] private GameObject _player;

    [SerializeField] private SpeechRecognitionTest speech;

    [SerializeField] private GameObject[] spellBook;

    private static MagicManager _instance;

    private const string GmLastSaid = "";

    private GameObject _spellToCast;

    [SerializeField] private float distance = 5f;



    public void GiveSpellXP(BaseSpell spell)
    {
        // give xp to that magic type
        // check if rank up

        SpellRanks tempRank = spell.GetThisSpellRank();
        int xpToGet = PlayerStatsManager.Instance.GetSpellXP(tempRank);
        Elements tempElement;

        switch (spell._element)
        {
            case Elements.Fire:
                Debug.Log("Giving Fire XP");
                PlayerStatsManager.Instance.fireLevel += xpToGet;
                tempElement = Elements.Fire;
                break;
            
            case Elements.Water:
                Debug.Log("Giving Water XP");
                PlayerStatsManager.Instance.waterLevel += xpToGet;
                tempElement = Elements.Water;
                break;
            
            case Elements.Earth:
                Debug.Log("Giving Earth XP");
                PlayerStatsManager.Instance.earthLevel += xpToGet;
                tempElement = Elements.Earth;
                break;
            
            case Elements.Wind:
                Debug.Log("Giving Wind XP");
                PlayerStatsManager.Instance.windLevel += xpToGet;
                tempElement = Elements.Wind;
                break;
            
            default:
                Debug.Log("No element found to give XP. Setting default type to fire");
                tempElement = Elements.Fire;
                break;
        }

        int levelToCheck = PlayerStatsManager.Instance.GetMagicTypeLevel(tempElement);
        
        PlayerStatsManager.Instance.CheckRankStatus(tempElement, tempRank);
    }


    private MagicManager() {
        _instance = this;
    }    
 
    public static MagicManager Instance 
    {
        get {
            if(_instance==null) 
                _instance = new MagicManager();

            return _instance;
        }
    }
    
    private readonly Dictionary<string, string> _spellBook = new Dictionary<string, string>()
    {
        // Key is the incantation, value is the spell name
        {"This is going to shoot a fireball", "Fireball spell"},
        {"I'm going to spawn a rock", "Rock Spell"},
        {"I would like a glass of water", "Water Spell"}
        
    };
    
    public void CastSpell(string incantation)
    {
        GetSpellFromIncantation(incantation);
        if (_spellToCast == null)
            return;
        
        //Instantiate(_spellToCast, castPoint.position, castPoint.rotation);
    }

    private void GetSpellFromIncantation(string incantation)
    {
        _spellToCast = null;

        int prevSmallestDistance = int.MaxValue;
        string keyToGet = "";
        int threshold = 10;
        
        foreach (var key in _spellBook.Keys) // loop through keys
        {
            var smallestDistance = CalculateLevenshteinDistance(key, incantation);
            if (smallestDistance < prevSmallestDistance) {
                // The distances are within the threshold of each other
                prevSmallestDistance = smallestDistance;
                keyToGet = key;
            }
        }
        
        // check if spell is valid within threshold
        if (prevSmallestDistance > threshold)
        {
            Debug.Log("No spell found...");
            return;
        }
        
        // Set spell prefab
        _spellToCast = GetSpellGameObject(_spellBook[keyToGet]);
        
        Debug.Log("Casting: " + _spellBook[keyToGet]);
    }

    
    
    // if (PlayerStatsManager.Instance.GetCurrentMana() >= _spellToCast.GetComponent<BaseSpell>().manaCost)
    // {
    //     //PlayerStatsManager.Instance.UseMana(GetManaCost(_spellToCast.GetComponent<BaseSpell>()));
    //     Instantiate(_spellToCast, castPoint.position, castPoint.rotation);
    //     GiveSpellXP(_spellToCast.GetComponent<BaseSpell>());
    // }
    // else
    //     Debug.Log("YOU HAVE NO MANA");

    public float GetManaCost(BaseSpell spell)
    {
        return spell.manaCost;
    }
    
    public float CalculateDamage(BaseSpell spell, int playerIntelligenceLevel)
    { 
        float totalDamage = 0;
        totalDamage = spell.baseDamage + playerIntelligenceLevel;

        return totalDamage;
    }


    public string GetLastSaid()
    {
        return GmLastSaid;
    }
    
    // Getting spell from voice
    


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
        
       // Debug.Log("No Spell found in GetSpellGameObject()");
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

        // Calculate rows and columns distances
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
        return matrix[source1Length, source2Length];
    }
}
