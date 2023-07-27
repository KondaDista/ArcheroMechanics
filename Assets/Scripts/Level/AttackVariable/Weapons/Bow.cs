using System;
using System.Threading.Tasks;
using UnityEngine;

public class Bow : Weapon
{
    [SerializeField] private JoystickPlayerExample _joystickPlayer;

    private void OnEnable()
    {
        _joystickPlayer.OnStay += Shoot;
    }

    private void OnDisable()
    {
        _joystickPlayer.OnStay -= Shoot;
    }
}
