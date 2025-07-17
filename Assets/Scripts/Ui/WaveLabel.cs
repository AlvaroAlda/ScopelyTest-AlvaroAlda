using EventChannels.WaveEvents;
using Managers.WaveData;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class WaveLabel : MonoBehaviour
    {
        [SerializeField] private Text waveText;
        [SerializeField] private WaveEvents waveEvents;

        private void OnEnable()
        {
            waveEvents.OnWaveStarted += OnWaveStarted;
        }

        private void OnDisable()
        {
            waveEvents.OnWaveStarted -= OnWaveStarted;
        }

        private void OnWaveStarted(WaveData waveData, int index)
        {
            waveText.text = (index + 1).ToString();
        }
    }
}