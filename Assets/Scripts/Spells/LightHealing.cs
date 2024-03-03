using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHealing : BaseSpell
{
    private void Awake()
    {
        name = "LightHealing";
        // set values in the editor
    }

    private void Start()
    {
        PlayerStatsUIManager.Instance.RestoreMana(5f);
        Destroy(this.gameObject);
    }

}
