using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MelleWeapon
{
    [SerializeField] protected OverlapAttack _melleWeapon;
    public override void PerformAttack()
    {
        _melleWeapon.PerformAttack();
    }
}
