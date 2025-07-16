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

    public void DestroyTarget()
    {
        TargetDestroyed = true;
        
        //Trigger event
        creepEvents.TriggerCreepDestroyed(creepData);

        _creepInitialized = true;
        gameObject.SetActive(false);
    }

    public void InitCreep(DefendingBase defendingBase, Vector3 spawningPoint)
    {
        _defendingBase = defendingBase;
        _currentLife = creepData.MaxLife;

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
        TargetDestroyed = false;
        TurretTargetProvider.AddActiveTarget(this);
    }

    protected override void OnDespawn()
    {
        TurretTargetProvider.RemoveActiveTarget(this);
        _currentLife = creepData.MaxLife;
    }
}