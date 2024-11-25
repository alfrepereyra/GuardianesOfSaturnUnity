using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float lifetime = 10f; // Tiempo antes de regresar al pool automáticamente
    private float timer;
    public int points = 10; // Puntos que da este enemigo al ser destruido


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

        public void OnBulletCollision()
    {
        // Agrega puntos al GameManager
        GameManager.Instance.AddScore(points);

    }
}
