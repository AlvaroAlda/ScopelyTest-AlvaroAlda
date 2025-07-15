using System;
using UnityEngine;

public class Creep : MonoBehaviour, ITarget
{
    [SerializeField] private CreepData creepData;
    [SerializeField] private CreepEvents creepEvents;

    private bool _creepInitialized;
    private float _currentLife;
    private float _hitTime;
    
    private Vector3 _targetPosition;
    private DefendingBase _defendingBase;
    
    public Vector3 TargetPosition => transform.position;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        _currentLife = creepData.MaxLife;
    }

    public void InitCreep(DefendingBase defendingBase, Vector3 spawningPoint)
    {
        _defendingBase = defendingBase;
        
        transform.position = spawningPoint;
        transform.LookAt(defendingBase.transform);

        _creepInitialized = true;
    }

    private void Update()
    {
        if (!_creepInitialized)
            return;
        
        var distance = Vector3.Distance(_defendingBase.transform.position, transform.position);
        if (distance <= creepData.HitRange)
            EvaluateHitBase();

        else
            transform.position += transform.forward * (Time.deltaTime * creepData.TravelSpeed);
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

    public void HitTarget(float damage)
    {
        _currentLife -= damage;
        if (_currentLife <= 0)
            DestroyTarget();
    }

    public void DestroyTarget()
    {
        creepEvents.TriggerCreepDestroyed(creepData);
        _creepInitialized = true;
        gameObject.SetActive(false);
    }
}
