using System;
using EventChannels.WaveEvents;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    [Header("Channels")] 
    [SerializeField] private CreepEvents creepEvents;
    [SerializeField] private WaveEvents waveEvents;
    
    [SerializeField] private WaveData[] waves;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private DefendingBase defendingBase;
    [SerializeField] private Creep creepToSpawn;

    private bool _waveRunning;
    private int _killedCreeps;
    private int _waveIndex = -1;
    private int _currentCreepsSpawned;
    
    private float _waveStartTime;
    private float _spawnCreepTime;
    
    private WaveData _currentWave;

    private void OnEnable()
    {
        creepEvents.OnCreepDestroyed += OnCreepDestroyed;
    }

    private void OnDisable()
    {
        creepEvents.OnCreepDestroyed -= OnCreepDestroyed;
    }

    private void Start()
    {
        StartNextWave();
    }

    private void Update()
    {
        if (!_currentWave || _currentCreepsSpawned >= _currentWave.TotalCreepsToSpawn)
            return;

        if (_waveStartTime < _currentWave.WaveStartDelay)
        {
            _waveStartTime += Time.deltaTime;
            return;
        }

        if (_spawnCreepTime < 1 / _currentWave.SpawnFrequency)
        {
            _spawnCreepTime += Time.deltaTime;
            return;
        }

        SpawnCreep();
    }
    
    private void OnCreepDestroyed(CreepData creep)
    {
        _killedCreeps++;
        if (_killedCreeps >= _currentWave.TotalCreepsToSpawn)
        {
            waveEvents.TriggerWaveFinished(_currentWave);
            StartNextWave();
        }
    }

    private void StartNextWave()
    {
        _waveIndex++;
        if (_waveIndex >= waves.Length)
        {
            GameManager.SharedInstance.WinGame();
            return;
        }
        
        _waveStartTime = 0;
        _currentCreepsSpawned = 0;
        _currentWave = waves[_waveIndex];
        
        waveEvents.TriggerWaveStarted(_currentWave, _waveIndex);
    }

    void SpawnCreep()
    {
        var creep = Instantiate(_currentWave.AvailableCreeps[Random.Range(0, _currentWave.AvailableCreeps.Length)]);
        creep.InitCreep(defendingBase, spawnPoints[Random.Range(0, spawnPoints.Length)].position);
        
        _spawnCreepTime = 0;
        _currentCreepsSpawned++;
    }
}
