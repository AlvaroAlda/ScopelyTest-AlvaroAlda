using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "TowerDefense/NewBullet", order = 0)]
public class BulletData : ScriptableObject
{
    public float BulletDamage;
    public float BulletSpeed;
}