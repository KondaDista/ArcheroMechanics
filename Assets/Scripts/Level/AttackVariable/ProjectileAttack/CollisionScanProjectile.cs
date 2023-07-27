
using UnityEngine;


public class CollisionScanProjectile : Projectile
{
    protected override void OnTargetCollider(Collider collider, IDamageable damageable)
    {
        damageable.ApplyDamage(Damage);
    }
}
