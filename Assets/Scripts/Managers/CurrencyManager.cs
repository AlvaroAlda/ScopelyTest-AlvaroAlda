using Creeps.CreepData;
using EventChannels.CreepEvents;
using EventChannels.CurrencyEvents;
using EventChannels.GameEvents;
using EventChannels.WaveEvents;
using UnityEngine;

namespace Managers
{
    public class CurrencyManager : Singleton<CurrencyManager>
    {
        [Header("Event Channels")] [SerializeField]
        private WaveEvents waveEvents;

        [SerializeField] private CreepEvents creepEvents;
        [SerializeField] private CurrencyEvents currencyEvents;
        [SerializeField] private GameEvents gameEvents;

        public int initCoins;

        private int _currentCoins;

        private int CurrentCoins
        {
            get => _currentCoins;
            set
            {
                _currentCoins = value;
                currencyEvents.TriggerCurrencyChanged(value);
            }
        }

        private void OnEnable()
        {
            gameEvents.OnGameStart += OnGameStart;
            waveEvents.OnWaveFinished += OnWaveFinished;
            creepEvents.OnCreepDestroyed += OnCreepDestroyed;
        }

        private void OnDisable()
        {
            gameEvents.OnGameStart -= OnGameStart;
            waveEvents.OnWaveFinished -= OnWaveFinished;
            creepEvents.OnCreepDestroyed -= OnCreepDestroyed;
        }

        private void OnWaveFinished(WaveData.WaveData waveData)
        {
            AddCurrency(waveData.WaveSuccessReward);
        }

        private void OnCreepDestroyed(CreepData creepData)
        {
            AddCurrency(creepData.Reward);
        }

        private void OnGameStart()
        {
            CurrentCoins = initCoins;
        }

        public bool TrySpendCurrency(int cost)
        {
            if (CurrentCoins < cost)
                return false;

            CurrentCoins -= cost;
            return true;
        }

        private void AddCurrency(int amount)
        {
            CurrentCoins += amount;
        }
    }
}