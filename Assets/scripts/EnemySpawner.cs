using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;     // Prefab del enemigo
    public float spawnInterval = 2f;   // Intervalo de tiempo entre spawns
    public float spawnRangeX = 8f;     // Rango de spawn en el eje X

    private float timer;

    void Update()
    {
        // Incrementa el temporizador
        timer += Time.deltaTime;

        // Spawnea un enemigo si el temporizador supera el intervalo
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

void SpawnEnemy()
{
    // Verifica si el prefab está asignado
    if (enemyPrefab == null)
    {
        Debug.LogError("Prefab de enemigo no asignado en el Inspector.");
        return;
    }

    // Calcula una posición aleatoria en el eje X dentro del rango y una posición fija en Z
    float randomX = Random.Range(-spawnRangeX, spawnRangeX);
    float spawnY = transform.position.y;  // Usamos la posición Y actual del spawner

    Vector3 spawnPosition = new Vector3(randomX, spawnY, -0.6f);  // Eje Z en -0.6

    // Instancia el enemigo en la posición calculada
    GameObject ClonEnemigo = Instantiate(enemyPrefab, spawnPosition, transform.rotation);
    Debug.Log("Enemigo spawneado en posición: " + spawnPosition);
}



}

