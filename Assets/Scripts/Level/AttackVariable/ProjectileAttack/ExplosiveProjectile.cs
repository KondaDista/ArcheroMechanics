using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : Projectile
{
    [Header("Explosion")] 
    [SerializeField] private Transform _overlapStartPoint;
    [SerializeField] private Vector3 _offset;
    [SerializeField, Min(0f)] private float _sphereRadius = 1f;
    
    [Header("Gizmos")] 
    [SerializeField] private Color _gizmosColor = Color.cyan;

    private readonly List<IDamageable> _explosionOverlapResults = new(32);

    protected override void OnProjectileDispose()
    {
        PerformExplosion();
    }

    private void PerformExplosion()
    {
        /*if (_explosionOverlapSettings.TryFind(_explosionOverlapResults))
        {
            _explosionOverlapResults.ForEach(ApplyDamage);
        }*/
    }

    private void ApplyDamage(IDamageable damageable)
    {
        damageable.ApplyDamage(Damage);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        TryDrawGizmos();
    }

    private void TryDrawGizmos()
    {
        if (_overlapStartPoint == null)
            return;

        Gizmos.matrix = _overlapStartPoint.localToWorldMatrix;
        Gizmos.color = _gizmosColor;


        Gizmos.DrawSphere(_offset, _sphereRadius);

    }
#endif
}