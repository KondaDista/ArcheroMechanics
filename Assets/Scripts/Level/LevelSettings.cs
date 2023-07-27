using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class LevelSettings : MonoBehaviour
{
    protected GameLevelController _gameLevelController;

    [SerializeField] private List<Enemy> _enemiesPrefab;
    [SerializeField] private List<Transform> _enemies;
    [SerializeField] private int _countEnemies;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Transform _player;
    [SerializeField] private Door door;
    [SerializeField] private Vector3 spawnPointPlayer = new Vector3(0, 1, 7);
    private Vector3 spawnPointEnemies;

    [Inject] private DiContainer diContainer;
    
    [Inject]
    private void Construct(GameLevelController gameLevelController)
    {
        _gameLevelController = gameLevelController;
    }

    private void OnEnable()
    {
        _gameLevelController.OnEnemyDeath += DeleteEnemies;
    }

    private void OnDisable()
    {
        _gameLevelController.OnEnemyDeath -= DeleteEnemies;
    }

    private void Start()
    {
        _player = SpawnCharacter(_playerPrefab, spawnPointPlayer);
        SpawnEnemies(_countEnemies);
    }

    private Transform SpawnCharacter(Character character, Vector3 spawnPoint)
    {
        return diContainer.InstantiatePrefab(character.transform, spawnPoint, Quaternion.identity, null).transform;
    }

    private void SpawnEnemies(int countEnemiesSpawn)
    {
        for (int i = 0; i < countEnemiesSpawn; i++)
        {
            spawnPointEnemies = new Vector3(Random.Range(-5, 6), 1, Random.Range(16, 36));
            _enemies.Add(SpawnCharacter(_enemiesPrefab[Random.Range(0, _enemiesPrefab.Count)], spawnPointEnemies));
        }
    }

    public void DeleteEnemies(Transform enemyDeleted)
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (_enemies[i] == enemyDeleted)
            {
                _enemies.Remove(enemyDeleted);
            }
        }

        if (IsEnemiesLives())
        {
            door.OpenDoor();
        }
    }

    private bool IsEnemiesLives()
    {
        if (_enemies.Count > 0)
            return false;
        else
            return true;
    }
}
