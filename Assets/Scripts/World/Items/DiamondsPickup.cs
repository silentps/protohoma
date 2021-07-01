using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondsPickup : PickupItem
{
    public override void OnTriggerEnterCallback()
    {
        base.OnTriggerEnterCallback();
        EconomyManager.Instance.AddDiamonds(1);
    }
}
