using System.Linq;
using ObjectPool;
using UnityEditor;
using UnityEngine;

public class Turret : BasePooledObject
{
    [SerializeField] private Transform muzzleTransform;
    [SerializeField] private TurretData turretData;
    [SerializeField] private Bullet bullet;
    [SerializeField] private GameEvents gameEvents;
    [SerializeField] private LineRenderer line;
    [SerializeField] private bool showDetectionGizmo;
    
    private float _shootingTimer;
    private ITurretTarget _currentTarget;
    
    public TurretData TurretData => turretData;
    
    private void OnGameStart()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_currentTarget == null || _currentTarget.TargetDestroyed ||
            Vector3.Distance(_currentTarget.TargetPosition, transform.position) > turretData.Range)
        {
            ActiveSearch();
        }

        else
        {
            ShootTarget();
        }
    }

    private void ShootTarget()
    {
        transform.LookAt(_currentTarget.TargetPosition);
        
        line.SetPosition(0, line.transform.position);
        line.SetPosition(1, new Vector3(_currentTarget.TargetPosition.x, line.GetPosition(1).y, _currentTarget.TargetPosition.z));
        
        if (_shootingTimer >= 1 / turretData.FiringRate)
        {
            _shootingTimer = 0;
            var shotBullet = (Bullet)PoolProvider.SharedInstance.GetPrefab(bullet);
            shotBullet.InitBullet(_currentTarget, muzzleTransform.position);
            shotBullet.gameObject.SetActive(true);
        }

        else
        {
            _shootingTimer += Time.deltaTime;
        }
    }

    private void ActiveSearch()
    {
        //Ensure cleaning current target reference
        line.enabled = false;
        _currentTarget = null;
        
        var activeTargets = TurretTargetProvider.ActiveTurretTargets;
        if (activeTargets.Count <= 0) 
            return;
        
        var closestTargets = activeTargets
            .Where(x => Vector3.Distance(x.TargetPosition , transform.position) <= turretData.Range)
            .ToList();

        if (closestTargets.Any())
        {
            _currentTarget = closestTargets[0];
            line.enabled = true;
        }
    }

    protected override void OnSpawn()
    {
        //Reset fire rate
        gameEvents.OnGameStart += OnGameStart;
        _shootingTimer = 1 / turretData.FiringRate;
    }

    protected override void OnDespawn()
    {
        gameEvents.OnGameStart -= OnGameStart;
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