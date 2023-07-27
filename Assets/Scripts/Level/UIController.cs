using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class UIController : MonoBehaviour
{
    protected GameLevelController _gameLevelController;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject winPanel;

    [Inject]
    private void Construct(GameLevelController gameLevelController)
    {
        _gameLevelController = gameLevelController;
        
    }
    private void OnEnable()
    {
        _gameLevelController.OnScoreChange += ChangeScore;
        _gameLevelController.OnPlayerDeath += ActiveDeathPanel;
        _gameLevelController.OnPlayerWin += ActiveWinPanel;
    }

    private void OnDisable()
    {
        _gameLevelController.OnScoreChange -= ChangeScore;
        _gameLevelController.OnPlayerDeath -= ActiveDeathPanel;
        _gameLevelController.OnPlayerWin -= ActiveWinPanel;
    }

    private void ChangeScore(int score)
    {
        scoreText.text = $"Coins: {score}";
    }
    
    private void ActiveDeathPanel()
    {
        deathPanel.SetActive(true);
    }
    private void ActiveWinPanel()
    {
        winPanel.SetActive(true);
    }
}
