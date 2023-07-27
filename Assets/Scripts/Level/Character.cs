using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class Character : MonoBehaviour, IDamageable
{
    public Action<float> OnHealthCahnged;

    [Header("Initial Parameters")]
    protected GameLevelController _gameLevelController;
    [SerializeField] protected float _maxHealth;
    [SerializeField] protected float _currentHealth;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected Weapon _weapon;
    
    
    [Inject]
    private void Construct(GameLevelController gameLevelController)
    {
        Debug.Log("Construct Character");
        _gameLevelController = gameLevelController;
    }
    
    public virtual void Start()
    {
        _currentHealth = _maxHealth;
        OnHealthCahnged?.Invoke(_currentHealth / _maxHealth);
    }
    
    public bool IsAlive()
    {
        return _currentHealth > 0f;
    }

    private void ChangeHealth()
    {
        if (IsAlive())
        {
            float _currentHealthAsPercantage = _currentHealth / _maxHealth;
            OnHealthCahnged?.Invoke(_currentHealthAsPercantage);
        }
        else
            Death();
    }

    protected virtual void Death()
    {
        OnHealthCahnged?.Invoke(0);
        Destroy(gameObject);
    }

    public void ApplyDamage(float damage)
    {
        if (damage < 0f)
            throw new ArgumentOutOfRangeException(nameof(damage));
        
        _currentHealth -= damage;
        ChangeHealth();
    }
    
    public void RotateToObject(Transform targetToRotate)
    {
        Vector3 direction = Vector3.RotateTowards(transform.forward, targetToRotate.position - transform.position, 50 * Time.deltaTime, 5);
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
