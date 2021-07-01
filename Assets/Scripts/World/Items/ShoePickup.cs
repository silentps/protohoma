using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoePickup : PickupItem
{
    public override void OnTriggerEnterCallback()
    {
        base.OnTriggerEnterCallback();
        Debug.Log("This is ShoePickup");
        FindObjectOfType<PlayerManager>().IncreaseShoe();
    }
}
