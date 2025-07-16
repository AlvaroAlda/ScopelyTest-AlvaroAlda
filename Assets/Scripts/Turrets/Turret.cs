using System;
using ObjectPool;
using UnityEngine;

public class Turret : BasePooledObject
{
    [SerializeField] private TurretData turretData;
    [SerializeField] private GameEvents gameEvents;
    [SerializeField] private SphereCollider detectionCollider;
    
    public TurretData TurretData => turretData;
    private ITurretTarget _currentTarget;

    private void Awake()
    {
        detectionCollider.radius = turretData.Range;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        gameEvents.OnGameStart += OnGameStart;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        gameEvents.OnGameStart -= OnGameStart;
    }

    private void OnGameStart()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var turretTarget = other.GetComponent<ITurretTarget>();
        if (turretTarget == null || turretTarget.TargetDestroyed)
            return;

        detectionCollider.enabled = false;
        _currentTarget = turretTarget;
    }

    private void Update()
    {
        if (_currentTarget == null || _currentTarget.TargetDestroyed)
            ActiveSearch();
        
        else
            ShootTarget();
    }

    private void ShootTarget()
    {
        
    }

    private void ActiveSearch()
    {
        if(!detectionCollider.enabled)
            detectionCollider.enabled = true;
    }

    protected override void OnSpawn() { }

    protected override void OnDespawn() { }
    
}