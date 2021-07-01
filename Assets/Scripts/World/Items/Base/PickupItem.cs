using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private void Start()
    {
        //Move object 0.5 up, with InOutCubic animation, pingpong looping and a random delay, so they don't move all of them at the same time
        gameObject.LeanMoveY(transform.position.y + 0.5f, 1).setEaseInOutCubic().setLoopPingPong().setDelay(Random.Range(0.0f,0.5f));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnTriggerEnterCallback();
            Destroy(gameObject);
        }
    }

    public virtual void OnTriggerEnterCallback()
    {

    }
}
