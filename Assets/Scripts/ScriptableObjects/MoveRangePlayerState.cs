using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "State", menuName = "States/MoveRangePlayer")]
public class MoveRangePlayerState : MoveState
{
    private Weapon weapon;
    public override void Initialize()
    {
        weapon = enemy.Weapon;
        if (weapon._closestUnit)
            enemy.NavRandomPoint(enemy.MoveDistance ,weapon._closestUnit.transform);
        else
            enemy.NavRandomPoint(enemy.MoveDistance, enemy.transform);
    }
    
    public override void MoveToTarget()
    {
        enemy.currentMovePosition.y = enemy.transform.position.y;
        float distance = (enemy.currentMovePosition - enemy.transform.position).magnitude;
        if (distance <= 1.5)
        {
            isFinished = true;
            enemy.SetState(enemy.stayState);
        }
    }
}
