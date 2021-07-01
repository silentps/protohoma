using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] GameObject _failMenu;
    [SerializeField] Button _restartButton;

    [SerializeField] GameObject _successMenu;
    [SerializeField] Button _continueButton;

    [SerializeField] GameObject _gameUI;

    [SerializeField] Image _leveLProgressionBar;
    [SerializeField] Text _leveLProgressionBarLevel;

    [SerializeField] Text _moneyDiamonds;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _restartButton.onClick.AddListener(GameManager.Instance.RestartGame);
        _continueButton.onClick.AddListener(GameManager.Instance.ContinueGame);
    }

    public void UpdateProgressionBar(float value)
    {
        if(value >= 0.0f && value <= 1.0f)
            _leveLProgressionBar.fillAmount = value;
    }

    public void UpdateProgressionBarLevel(string v)
    {
        _leveLProgressionBarLevel.text = v;
    }

    public void UpdateDiamondsText(string value)
    {
        _moneyDiamonds.text = value;
    }

    public void ActivateFailMenu()
    {
        _failMenu.SetActive(true);
    }

    public void ActivateSuccessMenu()
    {
        _successMenu.SetActive(true);
    }
}
