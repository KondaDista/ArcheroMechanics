using UnityEditor.UIElements;
using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(Rigidbody))]
public abstract class Projectile : MonoBehaviour
{
    [Header("Common")] 
    [SerializeField, Min(0f)] private float _damage = 10f;
    private string _tagParent;

    [Header("Rigidbody")] 
    [SerializeField] private Rigidbody _projectileRigidbody;

    [Header("Effect On Destroy")] 
    [SerializeField] private bool _spawnEffectOnDestroy = true;
    [SerializeField] private ParticleSystem _effectOnDestroyPrefab;
    [SerializeField, Min(0f)] private float _effectOnDestroyLifetime = 2f;
    
    public bool IsProjectileDisposed { get; private set; }
    public float Damage => _damage;
    public Rigidbody Rigidbody => _projectileRigidbody;

    public void Construct(float damage, string tagParent)
    {
        _damage = damage;
        _tagParent = tagParent;
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        if (IsProjectileDisposed)
            return;

        if (collider.gameObject.TryGetComponent(out IDamageable damageable) && !collider.CompareTag(_tagParent))
        {
            OnTargetCollider(collider, damageable);
        }

        OnAnyCollider(collider);
        DisposeProjectile();
    }

    public void DisposeProjectile()
    {
        OnProjectileDispose();

        SpawnEffectOnDestroy();

        Destroy(gameObject);

        IsProjectileDisposed = true;
    }

    private void SpawnEffectOnDestroy()
    {
        if (_spawnEffectOnDestroy == false)
            return;

        var effect = Instantiate(_effectOnDestroyPrefab, transform.position, _effectOnDestroyPrefab.transform.rotation);

        Destroy(effect.gameObject, _effectOnDestroyLifetime);
    }

    protected virtual void OnProjectileDispose() { }
    protected virtual void OnAnyCollider(Collider collider) { }
    protected virtual void OnTargetCollider(Collider collider, IDamageable damageable) { }
}
