using UnityEngine;

public class BalaScript : MonoBehaviour
{
    private BulletPool bulletPool; // Referencia al pool de balas
    private EnemySpawner enemySpawner; // Referencia al pool de enemigos
    public GameObject explosionPrefab; // Prefab de la explosión
    public float lifetime = 0.5f;    private void Awake()
    {
        // Buscar referencias automáticamente
        bulletPool = FindObjectOfType<BulletPool>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si colisiona con un enemigo
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            GameObject enemy = collision.gameObject;

            // Desactiva el enemigo y devuélvelo al pool
            enemySpawner.ReturnEnemyToPool(enemy);

            // Genera la explosión en la posición del enemigo
            SpawnExplosion(collision.transform.position);

            // Devuelve la bala al pool
            bulletPool.ReturnToPool(gameObject);
        }
    }
    void SpawnExplosion(Vector3 position)
    {
        GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        Destroy(explosion, lifetime); // explosion se destruirá, pero el prefab sigue intacto
    }
}