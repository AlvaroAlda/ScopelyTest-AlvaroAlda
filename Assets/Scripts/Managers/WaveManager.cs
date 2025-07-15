using EventChannels.WaveEvents;
using ObjectPool;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    [Header("Channels")] 
    [SerializeField] private CreepEvents creepEvents;

    [SerializeField] private WaveEvents waveEvents;
    [SerializeField] private GameEvents gameEvents;

    [SerializeField] private WaveData[] waves;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private DefendingBase defendingBase;
    private int _currentCreepsSpawned;

    private WaveData _currentWave;
    private int _killedCreeps;
    private float _spawnCreepTime;
    private int _waveIndex = -1;

    private bool _waveRunning;

    private float _waveStartTime;

    private void Update()
    {
        if (!_currentWave || _currentCreepsSpawned >= _currentWave.TotalCreepsToSpawn ||
            GameManager.SharedInstance.LevelFailed || GameManager.SharedInstance.LevelWin)
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

    private void OnEnable()
    {
        gameEvents.OnGameStart += OnGameStart;
        creepEvents.OnCreepDestroyed += OnCreepDestroyed;
    }

    private void OnDisable()
    {
        gameEvents.OnGameStart -= OnGameStart;
        creepEvents.OnCreepDestroyed -= OnCreepDestroyed;
    }

    private void OnGameStart()
    {
        _waveIndex = -1;
        StartNextWave();
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

    private void SpawnCreep()
    {
        var creepPrefab = _currentWave.AvailableCreeps[Random.Range(0, _currentWave.AvailableCreeps.Length)];
        
        var creep = (Creep)PoolProvider.SharedInstance.GetPrefab(creepPrefab);
        creep.InitCreep(defendingBase, spawnPoints[Random.Range(0, spawnPoints.Length)].position);

        _spawnCreepTime = 0;
        _currentCreepsSpawned++;
    }
}