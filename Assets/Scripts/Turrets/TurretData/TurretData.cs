using UnityEngine;

namespace Turrets.TurretData
{
    [CreateAssetMenu(fileName = "TurretData", menuName = "TowerDefense/NewTurret", order = 0)]
    public class TurretData : ScriptableObject
    {
        public float FiringRate;
        public int SpawnPrize;
        public float Range;
    
        [Header("Ui Selection Display")]
        public Sprite DisplaySprite;
    }
}
