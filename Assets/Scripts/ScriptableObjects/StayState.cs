using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "State", menuName = "States/Stay")]
public class StayState : State
{
    private float changePositionTime;

    public override void Initialize()
    {
        changePositionTime = enemy.ChangePositionTime;
        DelayMove();
    }
    public override void Run()
    {
        if (isFinished)
            return;
        Attack();
    }

    private async void DelayMove()
    {
        await Task.Delay(TimeSpan.FromSeconds(changePositionTime));
        isFinished = true;
        enemy.SetState(enemy.moveState);
    }

    private void Attack()
    {
        if (enemy.Weapon)
        {
            enemy.Weapon.Shoot();
        }
    }
}
