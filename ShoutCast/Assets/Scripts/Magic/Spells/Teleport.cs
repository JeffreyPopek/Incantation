using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : BaseSpell
{
    private void Awake()
    {
        name = "Teleport";
        // set values in the editor
    }

    private void Start()
    {
        //MagicManager.Instance.SetTeleportPosition(1);
        //MagicManager.Instance.SetPlayerPosition();
        Destroy(this.gameObject);
    }

}
