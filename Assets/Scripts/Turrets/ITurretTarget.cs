using UnityEngine;

namespace Turrets
{
    public interface ITurretTarget
    {
        public Vector3 TargetPosition { get; }
        public bool TargetDestroyed { get; }
        public void HitTarget(float damage);
    }
}