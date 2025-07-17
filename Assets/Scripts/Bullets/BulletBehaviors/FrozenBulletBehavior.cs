using Creeps;
using Creeps.StatusEffects;
using Turrets;
using UnityEngine;

namespace Bullets.BulletBehaviors
{
    [CreateAssetMenu(fileName = "FrozenBullet", menuName = "TowerDefense/Bullets/NewFrozenBullet", order = 0)]
    public class FrozenBulletBehavior : BaseBulletBehavior
    {
        [SerializeField] [Range(0, 1)] private float slowDownFactor = 0.2f;
        [SerializeField] private float slowDownDuration = 0.5f;
    
        public override void ReachTarget(Bullet bullet, ITurretTarget target)
        {
            var frozenEffect = new FreezeEffect(slowDownDuration, slowDownFactor);
        
            if (target is Creep creep)
                creep.AddEffect(frozenEffect);
        }
    }
}
