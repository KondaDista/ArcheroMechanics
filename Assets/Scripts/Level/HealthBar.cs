using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private Image _healthBarSprite;
    [SerializeField] private float _reduceSpeed = 2;
    private float _target = 1;
    private Camera _cameraMain;


    private void OnEnable()
    {
        _character.OnHealthCahnged += UpdateHealthBar;
    }

    private void OnDisable()
    {
        _character.OnHealthCahnged -= UpdateHealthBar;
    }

    private void Start()
    {
        _cameraMain = Camera.main;
    }

    private void UpdateHealthBar(float currentHealth)
    {
        _target = currentHealth;
    }

    private void LateUpdate()
    {
        transform.LookAt(new Vector3(transform.position.x, _cameraMain.transform.position.y, _cameraMain.transform.position.z));
        _healthBarSprite.fillAmount = Mathf.MoveTowards(_healthBarSprite.fillAmount, _target, _reduceSpeed * Time.deltaTime);
    }
    
}
