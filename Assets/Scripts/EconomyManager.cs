using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager Instance;
    int _currentDiamonds;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _currentDiamonds = PlayerPrefs.GetInt("Diamonds");
        UpdateDiamondsText();
    }

    public void AddDiamonds(int diamonds)
    {
        _currentDiamonds += diamonds; 
        UpdateDiamondsText();
        PlayerPrefs.SetInt("Diamonds", _currentDiamonds);
    }

    void UpdateDiamondsText()
    {
        UIManager.Instance.UpdateDiamondsText(_currentDiamonds.ToString());
    }
}
