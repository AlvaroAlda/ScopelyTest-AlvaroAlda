
using System;
using EventChannels.WaveEvents;
using UnityEngine;

public class CurrencyManager : Singleton<CurrencyManager>
{
    [Header("Event Channels")] 
    [SerializeField] private WaveEvents waveEvents;
    [SerializeField] private CreepEvents creepEvents;
    [SerializeField] private CurrencyEvents currencyEvents;

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
        waveEvents.OnWaveFinished += OnWaveFinished;
        creepEvents.OnCreepDestroyed += OnCreepDestroyed;
    }
    
    private void OnDisable()
    {
        waveEvents.OnWaveFinished -= OnWaveFinished;
        creepEvents.OnCreepDestroyed -= OnCreepDestroyed;
    }

    private void OnWaveFinished(WaveData waveData) => AddCurrency(waveData.WaveSuccessReward);

    private void OnCreepDestroyed(CreepData creepData) => AddCurrency(creepData.Reward);

    private void Start()
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

    void AddCurrency(int amount)
    {
        CurrentCoins += amount;
    }
}
