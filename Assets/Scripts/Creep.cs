using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creep : MonoBehaviour, ITarget
{
    [SerializeField] private CreepData creepData;
    [SerializeField] private CreepEvents creepEvents;
    
    private Vector3 _targetPosition;
    private float _currentLife;

    public Vector3 TargetPosition => transform.position;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        _currentLife = creepData.MaxLife;
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
        gameObject.SetActive(false);
    }
}
