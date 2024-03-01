using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagic : MonoBehaviour
{
    [SerializeField] private Transform castPoint;
    [SerializeField] private GameObject spellToCast;
    /*
     damage = attack * attack / defense
     attack = spell base damage + int level
     */
    
    public void CastSpell()
    {
        Instantiate(spellToCast, castPoint.position, castPoint.rotation);
    }
}
