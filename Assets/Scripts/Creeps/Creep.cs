using System.Collections.Generic;
using ObjectPool;
using UnityEngine;

public class Creep : BasePooledObject, ITurretTarget
{
    [SerializeField] private CreepData creepData;
    [SerializeField] private CreepEvents creepEvents;
    [SerializeField] private GameEvents gameEvents;
    
    private bool _creepInitialized;
    private float _currentLife;
    private float _hitTime;
    private DefendingBase _defendingBase;
    private List<BaseStatusEffect> _statusEffects = new(); 
    
    public float SpeedMultiplier { get; set; } = 1;
    public Vector3 TargetPosition => transform.position;
    public bool TargetDestroyed { get; private set; }
    

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
    
    public void InitCreep(DefendingBase defendingBase, Vector3 spawningPoint)
    {
        _defendingBase = defendingBase;
        _currentLife = creepData.MaxLife;

        transform.position = spawningPoint;
        transform.LookAt(defendingBase.transform);
        
        TargetDestroyed = false;
        TurretTargetProvider.AddActiveTarget(this);
        
        _creepInitialized = true;
    }
    
    private void OnGameStart()
    {
       gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!_creepInitialized || !gameObject.activeInHierarchy)
            return;

        UpdateStatusEffects();

        var distance = Vector3.Distance(_defendingBase.transform.position, transform.position);
        if (distance <= creepData.HitRange)
            EvaluateHitBase();

        else
            transform.position += transform.forward * (Time.deltaTime * creepData.TravelSpeed * SpeedMultiplier);
    }

    private void UpdateStatusEffects()
    {
        for (var i = 0; i < _statusEffects.Count; i++)
        {
            var effect = _statusEffects[i];
            effect.Update(this, Time.deltaTime);

            if (!effect.IsExpired) 
                continue;
            
            effect.Remove(this);
            _statusEffects.RemoveAt(i);
        }
    }

    public void AddEffect(BaseStatusEffect effect)
    {
        effect.Apply(this);
        _statusEffects.Add(effect);
    }

    public void HitTarget(float damage)
    {
        _currentLife -= damage;
        if (_currentLife <= 0)
            DestroyTarget();
    }

    public void DestroyTarget()
    {
        TargetDestroyed = true;
        
        //Trigger event
        creepEvents.TriggerCreepDestroyed(creepData);

        _creepInitialized = false;
        gameObject.SetActive(false);
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

    protected override void OnSpawn() { }

    protected override void OnDespawn()
    {
        TurretTargetProvider.RemoveActiveTarget(this);
        
        _currentLife = creepData.MaxLife;
        _statusEffects = new List<BaseStatusEffect>();
    }
}