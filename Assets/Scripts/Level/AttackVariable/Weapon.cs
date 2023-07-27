using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Weapon : AttackBehaviour
{
    [Header("Common")] 
    [SerializeField] private Transform _weaponMuzzle;
    [SerializeField, Min(0f)] private float _speed = 10f;
    [SerializeField, Min(0f)] private float _fireRate = 10f;
    [SerializeField] protected float _damageRange;
    [SerializeField] protected LayerMask _unitsLayer;
    
    [Header("Character")] 
    [SerializeField] protected Character _character;

    [Header("Bullet")] 
    [SerializeField] private Projectile _bulletPrefab;
    [SerializeField] protected float _damageBullet;
    
    private float nextTimeToFire = 1;
    public Transform _closestUnit;
    protected Collider[] _rangeHits = new Collider[3];

    protected bool _isRange => Physics.OverlapSphereNonAlloc(transform.position, _damageRange, _rangeHits, _unitsLayer) > 0;

    public virtual void Shoot()
    {
        if (_isRange)
        {
            FindClosestUnit();
            if (_closestUnit)
            {
                _character.RotateToObject(_closestUnit);
                if (CheckToSeeTarget(_closestUnit))
                {
                    if (Time.time >= nextTimeToFire)
                    {
                        nextTimeToFire = Time.time + 1 / _fireRate;
                        PerformAttack();
                    }
                }
            }
        }
    }
    
    protected bool CheckToSeeTarget(Transform target)
    {
#if UNITY_EDITOR
        Debug.DrawLine(transform.parent.position, target.position, Color.magenta);
#endif
        RaycastHit hit;
        Physics.Linecast(transform.parent.position, target.position, out hit);
        if (hit.collider.CompareTag(target.tag))
            return true;
        else
            return false;
    }

    public void FindClosestUnit()
    {
        float nearestDist = float.MaxValue;
        foreach (var hit in _rangeHits)
        {
            if (hit)
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                if (distance < nearestDist && CheckToSeeTarget(hit.transform))
                {
                    nearestDist = distance;
                    _closestUnit = hit.transform;
                }
            }
        }
    }

    public override void PerformAttack()
    {
        Projectile projectile = Instantiate(_bulletPrefab, _weaponMuzzle.position, _weaponMuzzle.rotation);
        projectile.Construct(_damageBullet, transform.parent.tag);
        projectile.Rigidbody.AddForce(_weaponMuzzle.forward * _speed, ForceMode.Impulse);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _damageRange);
        if (_closestUnit)
            Gizmos.DrawLine(transform.position, _closestUnit.position);
    }
#endif
}
