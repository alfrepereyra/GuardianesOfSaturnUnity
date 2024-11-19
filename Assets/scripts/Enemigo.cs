using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float lifetime = 10f; // Tiempo antes de regresar al pool automáticamente
    private float timer;

    void OnEnable()
    {
        timer = 0f; // Reinicia el temporizador al activarse
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Devuelve al pool si supera el tiempo de vida
        if (timer >= lifetime)
        {
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        // Encuentra el spawner y devuelve al enemigo al pool
        EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
        if (spawner != null)
        {
            spawner.ReturnEnemyToPool(gameObject);
        }
        else
        {
            Debug.LogError("No se encontró un EnemySpawner en la escena.");
        }
    }
}
