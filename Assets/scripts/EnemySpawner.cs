using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{   
    //prefab de enemigos
    public GameObject enemyPrefab;     
    public GameObject enemyType2Prefab; 
    public GameObject finalBossPrefab; 

    public int poolSize = 15;        
    public float spawnInterval = 2f;  
    public float spawnRangeX = 8f;    

    private Queue<GameObject> enemyPool = new Queue<GameObject>();  
    private Queue<GameObject> enemyType2Pool = new Queue<GameObject>(); 

    private float timer;
    private int currentEnemyType = 1; 

    private bool finalBossSpawned = false; 

    void Start()
    {
        //iniciael pool del enemigo 1
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            enemyPool.Enqueue(enemy);
        }

        // inicia el pool del enemigo 2
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyType2Prefab);
            enemy.SetActive(false);
            enemyType2Pool.Enqueue(enemy);
        }
    }

    void Update()
    {
        CheckScoreAndSwitchEnemyType();

        timer += Time.deltaTime;
        if (timer >= spawnInterval && !finalBossSpawned)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        if (currentEnemyType == 1 && enemyPool.Count > 0)
        {
            SpawnFromPool(enemyPool);
        }
        else if (currentEnemyType == 2 && enemyType2Pool.Count > 0)
        {
            SpawnFromPool(enemyType2Pool);
        }
    }

    void SpawnFromPool(Queue<GameObject> pool)
    {
        GameObject enemy = pool.Dequeue();
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, -0.6f);

        enemy.transform.position = spawnPosition;
        enemy.SetActive(true);

    }

    // Verifica el puntaje
    void CheckScoreAndSwitchEnemyType()
    {
        int score = GameManager.Instance.GetScore();

        if (score >= 1000 && score < 2000 && currentEnemyType != 2)
        {
            currentEnemyType = 2;
            Debug.Log("cambiando a enemigo tipo 2");
        }
        else if (score >= 2000 && !finalBossSpawned)
        {
            SpawnFinalBoss();
        }
    }
//spawnea al boss
void SpawnFinalBoss()
{
    finalBossSpawned = true;
    Vector3 bossPosition = new Vector3(0, transform.position.y, -0.6f);

    GameObject finalBoss = Instantiate(finalBossPrefab, bossPosition, Quaternion.identity);
    finalBoss.SetActive(true);
}

public void ReturnEnemyToPool(GameObject enemy, int type)
{
    enemy.SetActive(false);  
    if (type == 1)
    {   
        enemyPool.Enqueue(enemy);
    }
    else if (type == 2)
    {
        enemyPool.Enqueue(enemy);  
    }
}
}

