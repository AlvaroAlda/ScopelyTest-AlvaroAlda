using UnityEngine;

[CreateAssetMenu(fileName = "CreepData", menuName = "TowerDefense/CreepData", order = 0)]
public class CreepData : ScriptableObject
{
    public float TravelSpeed;
    public float MaxLife;

    [Header("Hit Info")] 
    public float HitDamage;
    public float HitRange;
    public float HitRate;

    [Header("Economy")] 
    public int Reward;
}