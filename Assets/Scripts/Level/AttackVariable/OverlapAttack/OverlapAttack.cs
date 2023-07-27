using System;
using UnityEngine;

public class OverlapAttack: AttackBehaviour
{
    public Action<Transform> OnPlayerMelleAttack;
    [Header("Common")] [SerializeField, Min(0f)]
    private float _damage = 10f;

    [Header("Masks")] [SerializeField] private LayerMask _searchLayerMask;
    [SerializeField] private LayerMask _obstacleLayerMask;

    [Header("Overlap Area")] [SerializeField]
    private Transform _overlapStartPoint;

    [SerializeField] private Vector3 _offset;
    [SerializeField, Min(0f)] private float _sphereRadius = 1f;

    [Header("Obstacles")] [SerializeField] private bool _considerObstacles;

    [Header("Gizmos")] [SerializeField] private Color _gizmosColor = Color.red;
    
    private Collider[] _overlapResults = new Collider[3];
    private int _overlapResultsCount;

    [ContextMenu(nameof(PerformAttack))]
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            OnPlayerMelleAttack?.Invoke(collider.transform);
            PerformAttack();
        }
    }
    public override void PerformAttack()
    {
        if (TryFindEnemies())
        {
            TryAttackEnemies();
        }
    }

    public bool TryFindEnemies()
    {
        var position = _overlapStartPoint.TransformPoint(_offset);

        _overlapResultsCount = OverlapSphere(position);

        return _overlapResultsCount > 0;
    }

    private int OverlapSphere(Vector3 position)
    {
        return Physics.OverlapSphereNonAlloc(position, _sphereRadius, _overlapResults, _searchLayerMask.value);
    }

    private void TryAttackEnemies()
    {
        for (int i = 0; i < _overlapResultsCount; i++)
        {
            if (_overlapResults[i].TryGetComponent(out IDamageable damageable) == false)
            {
                continue;
            }

            if (_considerObstacles)
            {
                var startPointPosition = _overlapStartPoint.position;
                var colliderPosition = _overlapResults[i].transform.position;
                var hasObstacle = Physics.Linecast(startPointPosition, colliderPosition,
                    _obstacleLayerMask.value);

                if (hasObstacle)
                {
                    continue;
                }
            }

            damageable.ApplyDamage(_damage);
        }
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
