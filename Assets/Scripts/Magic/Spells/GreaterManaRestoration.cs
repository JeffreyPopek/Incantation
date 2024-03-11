using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreaterManaRestoriation : BaseSpell
{
    private void Awake()
    {
        name = "GreaterManaRestoriation";
        // set values in the editor
    }

    private void Start()
    {
        PlayerStatsManager.Instance.RestoreMana(100f);
        Destroy(this.gameObject);
    }

}
