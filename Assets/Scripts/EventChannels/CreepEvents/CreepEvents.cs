using Creeps.CreepData;
using UnityEngine;
using UnityEngine.Events;

namespace EventChannels.CreepEvents
{
    [CreateAssetMenu(fileName = "CreepChannel", menuName = "TowerDefense/Channels/Creep", order = 0)]
    public class CreepEvents : ScriptableObject
    {
        public UnityAction<CreepData> OnCreepDestroyed = delegate { };

        public void TriggerCreepDestroyed(CreepData creepData)
        {
            OnCreepDestroyed.Invoke(creepData);
        }
    }
}