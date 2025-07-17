using Managers.WaveData;
using UnityEngine;
using UnityEngine.Events;

namespace EventChannels.WaveEvents
{
    [CreateAssetMenu(fileName = "WaveChannel", menuName = "TowerDefense/Channels/WaveChannel", order = 0)]
    public class WaveEvents : ScriptableObject
    {
        public UnityAction<WaveData> OnWaveFinished = delegate { };
        public UnityAction<WaveData, int> OnWaveStarted = delegate { };

        public void TriggerWaveFinished(WaveData waveData)
        {
            OnWaveFinished.Invoke(waveData);
        }

        public void TriggerWaveStarted(WaveData waveData, int waveIndex)
        {
            OnWaveStarted.Invoke(waveData, waveIndex);
        }
    }
}