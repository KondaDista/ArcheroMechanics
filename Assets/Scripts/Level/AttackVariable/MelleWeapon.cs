using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelleWeapon : Weapon
{ 
    private Enemy _enemy => (Enemy)_character;

    public override void PerformAttack()
    {
        _enemy.OverlapAttack.PerformAttack();
    }

}
