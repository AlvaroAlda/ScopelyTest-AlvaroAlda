using UnityEngine;

[CreateAssetMenu(fileName = "DamageBullet", menuName = "TowerDefense/Bullets/NewDamageBullet", order = 0)]
public class DamageBullet : BaseBulletBehavior
{
    [SerializeField] private float bulletDamage;
    public override void ReachTarget(Bullet bullet, ITurretTarget target)
    {
        target.HitTarget(bulletDamage);
    }
}
