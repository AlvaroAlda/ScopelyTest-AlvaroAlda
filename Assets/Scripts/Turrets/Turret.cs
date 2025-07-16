using System.Linq;
using ObjectPool;
using UnityEditor;
using UnityEngine;

public class Turret : BasePooledObject
{
    [SerializeField] private TurretData turretData;
    [SerializeField] private GameEvents gameEvents;
    [SerializeField] private bool showDetectionGizmo;
    
    private Collider[] _detectedColliders = new Collider[10];
    
    public TurretData TurretData => turretData;
    private ITurretTarget _currentTarget;

    private float _shootingTimer;

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
        Debug.Log("Detected");
        
        if (TryGetTarget(other, out var turretTarget)) 
            return;
        
        Debug.Log("TRIGGER DETECTED");
        _currentTarget = turretTarget;
    }

    private static bool TryGetTarget(Component other, out ITurretTarget turretTarget)
    {
        turretTarget = other.GetComponent<ITurretTarget>();
        return turretTarget == null || turretTarget.TargetDestroyed;
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
        transform.LookAt(_currentTarget.TargetPosition);
        
        if (_shootingTimer >= 1 / turretData.FiringRate)
        {
            _shootingTimer = 0;
            _currentTarget.HitTarget(20);
        }

        else
        {
            _shootingTimer += Time.deltaTime;
        }
    }

    private void ActiveSearch()
    {
        //Ensure cleaning current target reference
        _currentTarget = null;
        
        var activeTargets = TurretTargetProvider.ActiveTurretTargets;
        if (activeTargets.Count <= 0) 
            return;
        
        var closestTargets = activeTargets
            .Where(x => Vector3.Distance(x.TargetPosition , transform.position) <= turretData.Range)
            .ToList();

        if (closestTargets.Any())
            _currentTarget = closestTargets[0];
    }

    protected override void OnSpawn()
    {
        _shootingTimer = 1 / turretData.FiringRate;
    }

    protected override void OnDespawn()
    {
        _currentTarget = null;
        transform.rotation = Quaternion.identity;
    }
    
    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!showDetectionGizmo) 
            return;
        
        Handles.color = _currentTarget is { TargetDestroyed: false } ? Color.red :Color.green;
        Handles.DrawWireDisc(transform.position + Vector3.up * 0.2f, Vector3.up, turretData.Range);
    }
    #endif
}