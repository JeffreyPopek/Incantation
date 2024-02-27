using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    
    /*
     damage = attack * attack / defense
     attack = spell base damage + int level
     */
    private int healthLevel, intelligenceLevel;
    // Start is called before the first frame update
    void Start()
    {
        healthLevel = 10;
        intelligenceLevel = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetPlayerStats()
    {
        // Get player stats from character creation
    }
}
