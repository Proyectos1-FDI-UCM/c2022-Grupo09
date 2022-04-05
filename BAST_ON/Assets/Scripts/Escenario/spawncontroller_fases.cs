using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawncontroller_fases : MonoBehaviour
{
   public enum spawnState {SPAWNING, WAITING, COUNTING }
    [SerializeField]
    public class wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
        
    }
    public wave[] waves;
    private int nextwave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;

    private spawnState state = spawnState.COUNTING;

    private void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("no spawn points referenced");
        }
        waveCountdown = timeBetweenWaves;
    }
    private void Update()
    {
        if (state == spawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                wavecompleted();
            }
            else
            {
                return;
            }

        }
        if (waveCountdown <= 0)
        {
            if (state != spawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextwave]));
            }
            else
            {
                waveCountdown -= Time.deltaTime;
            }
        }

        IEnumerator SpawnWave(wave _wave)
        {
            Debug.Log("Spawning Wave" + _wave.name);
            state = spawnState.SPAWNING;
            for (int i = 0; i < _wave.count; i++)
            {
                SpawnEnemy(_wave.enemy);
                yield return new WaitForSeconds(1f / _wave.rate);

            }
            state = spawnState.WAITING;
            yield break;
        }
        void SpawnEnemy(Transform _enemy)
        {
            Debug.Log("Spawning Enemy:" + _enemy.name);

            Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(_enemy, _sp.position, _sp.rotation);
        }

    }
    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
        }
        if (GameObject.FindGameObjectsWithTag("Enemy") == null)
        {
            return false;
        }
        return true;
    }
    void wavecompleted()
    {
        Debug.Log("wave completed");

        state = spawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextwave + 1 > waves.Length - 1)
        {
            nextwave = 0;
            Debug.Log("ALL waves complete looping");
        }
        else
        {
            nextwave++;
        }
        bool EnemyIsAlive()
        {
            searchCountdown -= Time.deltaTime;
            if (searchCountdown <= 0f)
            {
                searchCountdown = 1f;
            }
            if (GameObject.FindGameObjectsWithTag("Enemy") == null)
            {
                return false;
            }
            return true;
        }
    }

    
}
