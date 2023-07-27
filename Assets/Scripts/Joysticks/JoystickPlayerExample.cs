using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class JoystickPlayerExample : MonoBehaviour
{
    public Action OnStay;

    [SerializeField] private Player _player;
    [SerializeField] private Transform _playerDirectionPrefab;
    private Transform _playerDirectionObject;
    private FloatingJoystick _joystick;
    private float _moveSpeed;
    private float _rotateSpeed = 50;
    private float _shiftDirection = 2.5f;

    private Rigidbody _rigidbody;
    private Vector3 _moveVector;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _joystick = FindObjectOfType<FloatingJoystick>();
        if (!_player)
            _player = GetComponent<Player>();
        _moveSpeed = _player.MoveSpeed;
        _playerDirectionObject = Instantiate(_playerDirectionPrefab, new Vector3(_player.transform.position.x, 0.1f, _player.transform.position.z), Quaternion.Euler(90,0,0));
    }

    private void Update()
    {
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            MovePlayer();
            RotatePlayer(_moveVector);
        }
        else
        {
            OnStay?.Invoke();
        }
        MoveDirectionPlayer();

    }

    private void MovePlayer()
    {
        _moveVector = Vector3.zero;
        _moveVector.x = _joystick.Horizontal * _moveSpeed * Time.deltaTime;
        _moveVector.z = _joystick.Vertical * _moveSpeed * Time.deltaTime;

        _rigidbody.MovePosition(_rigidbody.position + _moveVector);
    }

    public void RotatePlayer(Vector3 targetToRotate)
    {
        Vector3 direction = Vector3.RotateTowards(transform.forward, targetToRotate, _rotateSpeed * Time.deltaTime, 5.0f);
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void MoveDirectionPlayer()
    {
        _playerDirectionObject.localPosition = new Vector3(
            _player.transform.position.x + (_joystick.Horizontal * _shiftDirection), 
            _playerDirectionObject.localPosition.y, 
            _player.transform.position.z + (_joystick.Vertical * _shiftDirection));
    }
}