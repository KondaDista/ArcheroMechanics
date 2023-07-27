using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelController
{
    public Action<int> OnScoreChange;
    public Action<Transform> OnEnemyDeath;
    public Action OnPlayerDeath;
    public Action OnPlayerWin;
    private int _score;

    public void AddScore(int value)
    {
        if (value > 0)
            _score += value;
        OnScoreChange?.Invoke(_score);
    }

    public void PlayerWin()
    {
        OnPlayerWin?.Invoke();
    }
    
    public void PlayerDeath()
    {
        OnPlayerDeath?.Invoke();
    }
    
    public void EnemyDeath(Transform enemyTransform)
    {
        OnEnemyDeath?.Invoke(enemyTransform);
    }

}
