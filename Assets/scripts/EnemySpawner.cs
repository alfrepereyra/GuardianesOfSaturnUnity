using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;     // Prefab del enemigo 1
    public GameObject enemyType2Prefab; // Prefab del enemigo 2
    public GameObject finalBossPrefab; // Prefab del jefe final

    public int poolSize = 10;          // Tamaño del pool para los enemigos comunes
    public float spawnInterval = 2f;   // Intervalo de tiempo entre spawns
    public float spawnRangeX = 8f;     // Rango de spawn en el eje X

    private Queue<GameObject> enemyPool = new Queue<GameObject>();  // Pool de enemigos tipo 1
    private Queue<GameObject> enemyType2Pool = new Queue<GameObject>(); // Pool de enemigos tipo 2

    private float timer;
    private int currentEnemyType = 1; // Tipo de enemigo actual

    private bool finalBossSpawned = false; // Controla si el jefe final ya fue instanciado

    void Start()
    {
        // Inicializa el pool del enemigo 1
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            enemyPool.Enqueue(enemy);
        }

        // Inicializa el pool del enemigo 2
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyType2Prefab);
            enemy.SetActive(false);
            enemyType2Pool.Enqueue(enemy);
        }
    }

    void Update()
    {
        // Verifica el puntaje y cambia de tipo de enemigo
        CheckScoreAndSwitchEnemyType();

        // Solo spawnea un enemigo si el tiempo lo permite
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

        Debug.Log("Enemigo spawneado en posición: " + spawnPosition);
    }

    void CheckScoreAndSwitchEnemyType()
    {
        int score = GameManager.Instance.GetScore();

        if (score >= 1000 && score < 2000 && currentEnemyType != 2)
        {
            currentEnemyType = 2;
            Debug.Log("Cambiando a enemigo tipo 2");
        }
        else if (score >= 2000 && !finalBossSpawned)
        {
            SpawnFinalBoss();
        }
    }

    void SpawnFinalBoss()
    {
        finalBossSpawned = true;
        Vector3 bossPosition = new Vector3(0, transform.position.y, -0.6f);
        Instantiate(finalBossPrefab, bossPosition, Quaternion.identity);

        Debug.Log("¡Jefe final spawneado!");
    }

public void ReturnEnemyToPool(GameObject enemy, int type)
{
    enemy.SetActive(false);  // Desactiva el enemigo
    // Aquí puedes manejar los pools de enemigos según el tipo
    if (type == 1)
    {
        // Agregar a pool de Enemigo
        enemyPool.Enqueue(enemy);
    }
    else if (type == 2)
    {
        // Agregar a pool de Enemigo2 (si tienes un pool separado)
        enemyPool.Enqueue(enemy);  // Usa otro pool si es necesario
    }
}
}

