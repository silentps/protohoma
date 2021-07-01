using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    int _currentLevel;
    Vector3 _endPos;

    float _initialDistanceToEnd;

    public Vector3 EndPos { get => _endPos; set => _endPos = value; }
    public float InitialDistanceToEnd { get => _initialDistanceToEnd; set => _initialDistanceToEnd = value; }


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _currentLevel = PlayerPrefs.GetInt("Level");
        UIManager.Instance.UpdateProgressionBarLevel((_currentLevel + 1).ToString());
        GetComponent<LevelGenerator>().GenerateLevel(_currentLevel);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void ContinueGame()
    {
        _currentLevel++;
        PlayerPrefs.SetInt("Level", _currentLevel);
        RestartGame();
    }
}
