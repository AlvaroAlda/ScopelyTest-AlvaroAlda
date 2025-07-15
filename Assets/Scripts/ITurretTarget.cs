using UnityEngine;

public interface ITurretTarget
{
    public Vector3 TargetPosition { get; }
    public void HitTarget(float damage);
    public void DestroyTarget();
}