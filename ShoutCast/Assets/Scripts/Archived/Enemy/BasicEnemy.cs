using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using Random = UnityEngine.Random;

public class BasicEnemy : MonoBehaviour
{
    // Damager Text
    [SerializeField] private GameObject damageTextPrefab;
    [SerializeField] private Transform damageTextSpawnLocation;
    
    private int enemyLevel;
    private float currentHealth, maxHealth;

    // UI
    [SerializeField] private FloatingHealthBar healthBar;
    [SerializeField] private TextMeshProUGUI healthValueText;
    [SerializeField] private TextMeshProUGUI levelValueText;

    private void Start()
    {
        // Set random Level
        enemyLevel = Random.Range(1, 50);
        levelValueText.text = enemyLevel.ToString();
        
        // Set health according to level
        currentHealth = 10 + (enemyLevel * 10);
        maxHealth = currentHealth;
        
        healthBar.GetComponentInChildren<FloatingHealthBar>();
        healthBar.UpdateHealthbar(currentHealth, maxHealth);

        healthValueText.text = currentHealth.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Magic"))
        {
            TakeDamage(MagicManager.Instance.CalculateDamage(other.GetComponent<BaseSpell>(), 1));
        }
    }

    private void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        healthBar.UpdateHealthbar(currentHealth, maxHealth);
        healthValueText.text = currentHealth.ToString();

        
        // Show Damage Number
        GameObject damageText = Instantiate(damageTextPrefab, damageTextSpawnLocation);
        damageText.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(damage.ToString());
        
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }

    }
}
