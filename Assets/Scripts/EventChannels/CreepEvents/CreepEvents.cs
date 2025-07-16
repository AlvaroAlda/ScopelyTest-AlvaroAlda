using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "CreepChannel", menuName = "TowerDefense/Channels/Creep", order = 0)]
public class CreepEvents : ScriptableObject
{
    public UnityAction<CreepData> OnCreepDestroyed = delegate { };

    public void TriggerCreepDestroyed(CreepData creepData)
    {
        OnCreepDestroyed.Invoke(creepData);
    }
}