using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] Text timeText;

    [SerializeField] Transform[] allEnemisAvaiable;

    int levelOfWavesCounter;

    public enum SpawnState { SPAWNING, WAITING, COUNTING};

    [System.Serializable]
    public class Wave
    {
        public string name;//name of wave
        public List<Transform> enemies = new List<Transform>();
        public int enemyAmount;
        public float spawnRate;
    }

    public Wave[] waves;
    int nextWave = 0; //stores and index of wave

    public float timeBetweenWaves = 5f;
    float waveCountdown;

    SpawnState state = SpawnState.COUNTING;

    float searchCountdown = 1f;

    GameObject player;
    GameObject tower;
    [SerializeField] float distanceBetweenNotToSpawnEnemies;

    float counterToAnimation;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        tower = GameObject.FindGameObjectWithTag("Tower");
        waveCountdown = timeBetweenWaves;
    }

    private void Update()
    {
        if (tower.gameObject == null) return;

        if(state == SpawnState.WAITING)
        {
            if(!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if(waveCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;


            if (counterToAnimation > 0)
            {
                counterToAnimation -= Time.deltaTime;
            }
            else
            {
                counterToAnimation = 1f;
                timeText.text = waveCountdown.ToString("0");
                animator.Play("CountDownTime");
            }

        }
    }

    void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        //if all waves ended
        if(nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            //can add stat multiplier here
            Debug.Log("All waves complete! Looping...");


            foreach (Wave wave in waves)
            {
                wave.enemyAmount += Random.Range(1, 3);

                if(levelOfWavesCounter < allEnemisAvaiable.Length)
                {
                    wave.enemies.Add(allEnemisAvaiable[levelOfWavesCounter]);
                }
            }

            levelOfWavesCounter++;

        }
        else
        {
            nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0f)
        {
            searchCountdown = 1f;

            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.name);
        state = SpawnState.SPAWNING;

        for(int i = 0; i < _wave.enemyAmount; i++)
        {
            SpawnEnemy(_wave.enemies[Random.Range(0, _wave.enemies.Count)]);
            yield return new WaitForSeconds(1f / _wave.spawnRate); // 1 / 10 = 0.1sec
        }


        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning enemy " + _enemy.name);

        Vector2 randomPos = new Vector2(Random.Range(-30f, 30f), Random.Range(-25f, 25f));


        float distanceToPlayer = Vector2.Distance(player.transform.position, randomPos);
        float distanceToTower = Vector2.Distance(tower.transform.position, randomPos);
        while (distanceToPlayer < distanceBetweenNotToSpawnEnemies && distanceToTower < distanceBetweenNotToSpawnEnemies)
        {
            randomPos = new Vector2(Random.Range(-30f, 30f), Random.Range(-25f, 25f));
            distanceToPlayer = Vector2.Distance(player.transform.position, randomPos);
            distanceToTower = Vector2.Distance(tower.transform.position, randomPos);
            Debug.Log("Looping while");
        }


        Instantiate(_enemy, randomPos, Quaternion.identity);
    }
}
