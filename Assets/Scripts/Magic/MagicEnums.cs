using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public class MagicEnums : MonoBehaviour {}
