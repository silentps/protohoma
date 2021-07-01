using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    int _platformHeight;

    private void Start()
    {
        _platformHeight = 4;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnTriggerEnterCallback();
        }
    }
    public void OnTriggerEnterCallback()
    {
        FindObjectOfType<PlayerManager>().CutShoes(_platformHeight);
    }
}
