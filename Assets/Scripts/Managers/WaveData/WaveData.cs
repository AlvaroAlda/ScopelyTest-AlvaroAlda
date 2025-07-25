using Creeps;
using UnityEngine;

namespace Managers.WaveData
{
    [CreateAssetMenu(fileName = "WaveData", menuName = "TowerDefense/NewWave", order = 0)]
    public class WaveData : ScriptableObject
    {
        public Creep[] AvailableCreeps;
        public float WaveStartDelay;
        public float SpawnFrequency;
        public int TotalCreepsToSpawn;
        public int WaveSuccessReward;
    }
}