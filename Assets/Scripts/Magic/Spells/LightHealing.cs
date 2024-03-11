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
        PlayerStatsManager.Instance.Heal(5f);
        Destroy(this.gameObject);
    }

}
