using ObjectPool;
using UnityEngine;

public class Turret : BasePooledObject
{
    [SerializeField] private TurretData turretData;
    public TurretData TurretData => turretData;


    protected override void OnSpawn() { }

    protected override void OnDespawn() { }
}