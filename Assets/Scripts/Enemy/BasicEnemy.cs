using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    private int healthLevel;
    private int currentHealth, MaxHealth;



    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = healthLevel + 10;
        currentHealth = MaxHealth;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // private void OnCollisionEnter (Collider target)
    // {
    //     if(target.gameObject.tag.Equals("PlayerAttack"))
    //     {
    //         // Find which attack is what and apply damage
    //         int totalDamage;
    //         
    //         // get spell tier + base damage
    //         // call a "calculateDamage function
    //         // take the damage
    //     }
    // }


    
}
