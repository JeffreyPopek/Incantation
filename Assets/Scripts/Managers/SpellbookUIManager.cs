using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpellbookUIManager : MonoBehaviour
{
    [SerializeField] private GameObject spellbookBG;
    private bool spellbookBGActive;

    [SerializeField] private TextMeshProUGUI fireRank;
    [SerializeField] private TextMeshProUGUI waterRank;
    [SerializeField] private TextMeshProUGUI earthRank;
    [SerializeField] private TextMeshProUGUI windRank;

    private void Start()
    {
        UpdateUIText();
        spellbookBG.SetActive(false);
        spellbookBGActive = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleUI();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            PlayerStatsManager.Instance.set();
        }
    }

    private void ToggleUI()
    {
        if (!spellbookBGActive)
        {
            // Only update text when UI is shown
            UpdateUIText();
            spellbookBG.SetActive(true);
            
            // Set bool for next function call
            spellbookBGActive = true;
        }
        else
        {
            spellbookBG.SetActive(false);
            
            // Set bool for next function call
            spellbookBGActive = false;
        }
    }

    private void UpdateUIText()
    {
        fireRank.text = "Fire Magic: " + PlayerStatsManager.Instance.fireRank;
        waterRank.text = "Water Magic: " + PlayerStatsManager.Instance.waterRank;
        earthRank.text = "Earth Magic: " + PlayerStatsManager.Instance.earthRank;
        windRank.text = "Wind Magic: " + PlayerStatsManager.Instance.windRank;
    }
}
