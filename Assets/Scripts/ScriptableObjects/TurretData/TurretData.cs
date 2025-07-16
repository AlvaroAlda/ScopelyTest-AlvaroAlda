using UnityEngine;


[CreateAssetMenu(fileName = "TurretData", menuName = "TowerDefense/NewTurret", order = 0)]
public class TurretData : ScriptableObject
{
    public Bullet Bullet;
    public float FiringRate;
    public int SpawnPrize;
    public float Range;
    
    [Header("Ui Selection Display")]
    public Sprite DisplaySprite;
}
