using Turrets;
using UnityEngine;

namespace Bullets.BulletBehaviors
{
    public abstract class BaseBulletBehavior : ScriptableObject
    {
        [SerializeField] protected float bulletSpeed;
        [SerializeField] [Tooltip("-1: infinite")]protected float lifeTime = -1;

        private float _currentLifeTime;
    
        public abstract void ReachTarget(Bullet bullet, ITurretTarget target);
        public virtual void Travel(Bullet bullet, Vector3 direction)
        {
            bullet.transform.position += direction * (bulletSpeed * Time.deltaTime);

            if (lifeTime > 0)
            {
                _currentLifeTime += Time.deltaTime;
            
                if (_currentLifeTime >= lifeTime)
                    bullet.gameObject.SetActive(false);
            }
        }

        public virtual void ResetBullet(Bullet bullet)
        {
            _currentLifeTime = 0;
        }
    }
}
