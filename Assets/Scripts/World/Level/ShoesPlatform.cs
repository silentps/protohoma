using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoesPlatform : MonoBehaviour
{
    [SerializeField] GameObject[] _shoes;
    // Start is called before the first frame update
    void Start()
    {
        int randomSide = Random.Range(0, _shoes.Length);
        _shoes[randomSide].SetActive(true);
    }
}
