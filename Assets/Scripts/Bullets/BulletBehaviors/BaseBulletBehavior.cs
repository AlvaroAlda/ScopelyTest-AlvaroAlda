using UnityEngine;

public abstract class BaseBulletBehavior : ScriptableObject
{
    [SerializeField] protected float bulletSpeed;
    
    public abstract void ReachTarget(Bullet bullet, ITurretTarget target);
    public virtual void Travel(Bullet bullet, Vector3 direction)
    {
        bullet.transform.position += direction * (bulletSpeed * Time.deltaTime);
    }
}
