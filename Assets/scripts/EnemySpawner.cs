using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;     // Prefab del enemigo
    public int poolSize = 10;          // Tamaño del pool de enemigos
    public float spawnInterval = 2f;   // Intervalo de tiempo entre spawns
    public float spawnRangeX = 8f;     // Rango de spawn en el eje X

    private Queue<GameObject> enemyPool = new Queue<GameObject>(); // Pool de enemigos
    private float timer;

    void Start()
    {
        // Inicializa el pool de enemigos
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);  // Desactiva el enemigo inicialmente
            enemyPool.Enqueue(enemy); // Lo agrega al pool
        }
    }

    void Update()
    {
        // Solo spawnea un enemigo si el tiempo lo permite
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;  // Resetea el temporizador
        }
    }

    void SpawnEnemy()
    {
        // Solo saca un enemigo del pool si hay disponibles
        if (enemyPool.Count > 0)
        {
            GameObject enemy = enemyPool.Dequeue();
            float randomX = Random.Range(-spawnRangeX, spawnRangeX); // Posición aleatoria en X
            Vector3 spawnPosition = new Vector3(randomX, transform.position.y, -0.6f);  // Posición fija en Y y Z

            enemy.transform.position = spawnPosition;
            enemy.SetActive(true);  // Activa el enemigo

            Debug.Log("Enemigo spawneado en posición: " + spawnPosition);
        }
        else
        {
            Debug.LogWarning("No hay enemigos disponibles en el pool.");
        }
    }

    // Devuelve un enemigo al pool cuando se destruye o se desactiva
    public void ReturnEnemyToPool(GameObject enemy)
    {
        enemy.SetActive(false);  // Desactiva el enemigo
        enemyPool.Enqueue(enemy); // Lo regresa al pool
    }
}
