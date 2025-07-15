using UnityEngine;

public interface ITarget
{
    public Vector3 TargetPosition { get; }
    public void HitTarget(float damage);
    public void DestroyTarget();
}