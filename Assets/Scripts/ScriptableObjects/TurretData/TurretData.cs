using UnityEngine;


[CreateAssetMenu(fileName = "TurretData", menuName = "TowerDefense/NewTurret", order = 0)]
public class TurretData : ScriptableObject
{
    public Bullet Bullet;
    public float FiringRate;
    public float Range;
}
