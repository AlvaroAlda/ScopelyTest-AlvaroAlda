using System;
using ObjectPool;
using UnityEngine;

public class Creep : BasePooledObject, ITurretTarget
{
    [SerializeField] private CreepData creepData;
    [SerializeField] private CreepEvents creepEvents;
    [SerializeField] private GameEvents gameEvents;

    private bool _creepInitialized;
    private float _currentLife;
    private DefendingBase _defendingBase;
    private float _hitTime;

    private Vector3 _targetPosition;
    public Vector3 TargetPosition => transform.position;

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

    private void Update()
    {
        if (!_creepInitialized || !gameObject.activeInHierarchy)
            return;

        var distance = Vector3.Distance(_defendingBase.transform.position, transform.position);
        if (distance <= creepData.HitRange)
            EvaluateHitBase();

        else
            transform.position += transform.forward * (Time.deltaTime * creepData.TravelSpeed);
    }
    
    public void HitTarget(float damage)
    {
        _currentLife -= damage;
        if (_currentLife <= 0)
            DestroyTarget();
    }

    private void OnMouseDown()
    {
        DestroyTarget();
    }

    public void DestroyTarget()
    {
        //Trigger event
        creepEvents.TriggerCreepDestroyed(creepData);

        _creepInitialized = true;
        gameObject.SetActive(false);
    }

    public void InitCreep(DefendingBase defendingBase, Vector3 spawningPoint)
    {
        _defendingBase = defendingBase;

        transform.position = spawningPoint;
        transform.LookAt(defendingBase.transform);

        _creepInitialized = true;
    }

    private void EvaluateHitBase()
    {
        if (_hitTime >= 1 / creepData.HitRate)
        {
            _defendingBase.HitBase(creepData.HitDamage);
            _hitTime = 0;
        }
        else
        {
            _hitTime += Time.deltaTime;
        }
    }

    protected override void OnSpawn()
    {
        _currentLife = creepData.MaxLife;
    }

    protected override void OnDespawn()
    {
        _currentLife = creepData.MaxLife;
    }
}