using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "State", menuName = "States/Move")]
public class MoveState : State
{
    public override void Initialize()
    {
        enemy.NavRandomPoint(enemy.MoveDistance, enemy.transform);
    }

    public override void Run()
    {
        if (isFinished)
            return;
        MoveToTarget();
    }

    public virtual void MoveToTarget()
    {
        float distance = (enemy.currentMovePosition - enemy.transform.position).magnitude;
        if (distance <= 1.5)
        {
            isFinished = true;
            enemy.Weapon._closestUnit = null;
            enemy.Weapon.FindClosestUnit();
            enemy.SetState(enemy.stayState);
        }
    }
    
    
}
