using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    public override void Update()
    {
        base.Update();
        if (Weapon._closestUnit)
        {
            RotateToObject(Weapon._closestUnit);
        }
    }
}
