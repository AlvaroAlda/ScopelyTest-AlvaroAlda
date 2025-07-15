using UnityEngine;

[CreateAssetMenu(fileName = "CreepData", menuName = "TowerDefense/CreepData", order = 0)]
public class CreepData : ScriptableObject
{
    public float TravelSpeed;
    public float MaxLife;
    public float HitDamage;
    public int Reward;
}
