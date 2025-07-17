using System.Linq;
using Bullets.BulletBehaviors;
using CustomObjectPool;
using EventChannels.GameEvents;
using Turrets;
using UnityEngine;

namespace Bullets
{
    public class Bullet : BasePooledObject
    {
        [SerializeField] private float distanceThreshold;
        [SerializeField] private BaseBulletBehavior bulletBehavior;
        [SerializeField] private GameEvents gameEvents;

        private ITurretTarget _target;
        private Vector3 _targetShootingPosition;
        private Vector3 _direction;
    
        private void OnGameStart()
        {
            gameObject.SetActive(false);
        }

        public void InitBullet(ITurretTarget target, Vector3 spawnPosition)
        {
            _target = target;
            transform.position = spawnPosition;

            _targetShootingPosition = new Vector3(_target.TargetPosition.x, transform.position.y, _target.TargetPosition.z);
            _direction = (_targetShootingPosition - transform.position).normalized;
        }

        private void Update()
        {
            bulletBehavior.Travel(this, _direction);

            var activeTargets = TurretTargetProvider.ActiveTurretTargets;
            if (activeTargets.Count <= 0)
                return;
        
            var closestTargets = activeTargets
                .Where(x => Vector3.Distance(x.TargetPosition , transform.position) <= distanceThreshold)
                .ToList();

            if (closestTargets.Any())
            {
                bulletBehavior.ReachTarget(this, closestTargets[0]);
                gameObject.SetActive(false);
            }
        }

        protected override void OnSpawn()
        {
            gameEvents.OnGameStart += OnGameStart;
        }

        protected override void OnDespawn()
        {
            gameEvents.OnGameStart -= OnGameStart;
            bulletBehavior.ResetBullet(this);
        }
    }
}
