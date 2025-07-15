using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "TowerDefense/WaveData", order = 0)]
public class WaveData : ScriptableObject
{
    public Creep[] AvailableCreeps;
    public float WaveStartDelay;
    public float SpawnFrequency;
    public int TotalCreepsToSpawn;
    public int WaveSuccessReward;
}
