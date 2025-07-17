using UnityEngine;

public abstract class BaseBulletBehavior : ScriptableObject
{
    [SerializeField] protected float bulletSpeed;
    [SerializeField] [Tooltip("-1: infinite")]protected float lifeTime = -1;

    protected float CurrentLifeTime;
    
    public abstract void ReachTarget(Bullet bullet, ITurretTarget target);
    public virtual void Travel(Bullet bullet, Vector3 direction)
    {
        bullet.transform.position += direction * (bulletSpeed * Time.deltaTime);

        if (lifeTime > 0)
        {
            CurrentLifeTime += Time.deltaTime;
            
            if (CurrentLifeTime >= lifeTime)
                bullet.gameObject.SetActive(false);
        }
    }

    public virtual void ResetBullet(Bullet bullet)
    {
        CurrentLifeTime = 0;
    }
}
