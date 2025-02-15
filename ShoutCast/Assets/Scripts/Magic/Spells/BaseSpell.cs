using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public abstract class BaseSpell : MonoBehaviour
{
    // public struct Elements
    // {
    //     private int FIRE;
    //     private int 
    // }
    private bool hasLearnt;
    private string name;
    public float baseDamage;
    public float manaCost;
    public int requiredLevelToCast;
    public float speed;
    public float lifetime;
    public float radius;
    public SpellRanks _spellRank;
    public Elements _element;

    public virtual bool GetHasLearnt()
    {
        return hasLearnt;
    }

    public virtual SpellRanks GetThisSpellRank()
    {
        return _spellRank;
    }

    public virtual void SetHasLearnt()
    {
        hasLearnt = false;
    }
}
