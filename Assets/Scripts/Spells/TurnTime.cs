using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTime : BaseSpell
{
    private void Awake()
    {
        name = "TurnTime";
        // set values in the editor
    }

    private void Start()
    {
        if (DayAndNightManager.Instance.GetTimeScale() == 30f)
        {
            DayAndNightManager.Instance.SetTimeScale(1f);
        }
        else
        {
            Debug.Log("Already changing time!");
        }
        Destroy(this.gameObject);

        
    }





}
