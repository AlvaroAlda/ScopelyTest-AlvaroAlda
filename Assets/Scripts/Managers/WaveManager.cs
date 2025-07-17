using System.Collections;
using Creeps;
using Creeps.CreepData;
using CustomObjectPool;
using EventChannels.CreepEvents;
using EventChannels.GameEvents;
using EventChannels.WaveEvents;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class WaveManager : MonoBehaviour
    {
        [Header("Channels")] 
        [SerializeField] private CreepEvents creepEvents;
        [SerializeField] private WaveEvents waveEvents;
        [SerializeField] private GameEvents gameEvents;

        [Header("Waves References")]
        [SerializeField] private WaveData.WaveData[] waves;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private DefendingBase defendingBase;
        
        private bool _waveRunning;
    
        private int _waveIndex = -1;
        private int _currentCreepsSpawned;
        private int _killedCreeps;
        
        private Coroutine _spawnCoroutine;
        private WaveData.WaveData _currentWave;
        
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
            StopSpawningCoroutine();
            StartNextWave();
        }
        
        IEnumerator SpawnCreepCoroutine()
        {
            yield return new WaitForSeconds(_currentWave.WaveStartDelay);
            while (_currentCreepsSpawned < _currentWave.TotalCreepsToSpawn && !GameManager.SharedInstance.LevelFailed) 
            {
                SpawnCreep();
                yield return new WaitForSeconds(1 / _currentWave.SpawnFrequency);
            }
        }
        
        void StopSpawningCoroutine()
        {
            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
                _spawnCoroutine = null;
            }
        }

        private void OnCreepDestroyed(CreepData creep)
        {
            _killedCreeps++;
            if (_killedCreeps >= _currentWave.TotalCreepsToSpawn)
            {
                _killedCreeps = 0;
                waveEvents.TriggerWaveFinished(_currentWave);
                StopSpawningCoroutine();
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
            
            _killedCreeps = 0;
            _currentCreepsSpawned = 0;
            _currentWave = waves[_waveIndex];
            
            _spawnCoroutine = StartCoroutine(SpawnCreepCoroutine());
            waveEvents.TriggerWaveStarted(_currentWave, _waveIndex);
        }

        private void SpawnCreep()
        {
            var creepPrefab = _currentWave.AvailableCreeps[Random.Range(0, _currentWave.AvailableCreeps.Length)];
            var creep = (Creep)PoolProvider.SharedInstance.GetPrefab(creepPrefab);
            
            creep.gameObject.SetActive(true);
            creep.InitCreep(defendingBase, spawnPoints[Random.Range(0, spawnPoints.Length)].position);
            
            _currentCreepsSpawned++;
        }
    }
}