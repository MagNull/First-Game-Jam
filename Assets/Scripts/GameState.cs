using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private RoadMover _roadMover;
    [SerializeField] private PointsCounter _pointsCounter;
    [SerializeField] private TextMeshProUGUI _bestScoreText;
    private float _bestScore = 0;

    private void Start()
    {
        _loseScreen.SetActive(false);
        _roadMover.gameObject.SetActive(false);
        _pointsCounter.gameObject.SetActive(false);
        _bestScore = PlayerPrefs.GetInt("Best Score");
        _bestScoreText.text = $"Best Score: {PlayerPrefs.GetInt("Best Score")}";
    }

    public void StartGame()
    {
        if (!_roadMover.gameObject.activeSelf)
        {
            _roadMover.gameObject.SetActive(true);
            _pointsCounter.gameObject.SetActive(true);
        }
    }

    public void Lose()
    {
        Time.timeScale = 0;
        _loseScreen.SetActive(true);
        _bestScore = Mathf.Max(_bestScore, _pointsCounter.Points);
        _bestScoreText.text = $"Best Score: {(int)_bestScore}";
        PlayerPrefs.SetInt("Best Score", (int)_bestScore);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit(); 
    }
}
