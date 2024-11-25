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
    // Verificar si colisiona con el primer enemigo
    if (collision.gameObject.CompareTag("Enemigo"))
    {
        Enemigo enemigo = collision.gameObject.GetComponent<Enemigo>();
        HandleEnemyCollision(enemigo, collision, 1); // Tipo 1 para Enemigo
    }
    // Verificar si colisiona con el segundo enemigo
    else if (collision.gameObject.CompareTag("Enemigo2"))
    {
        Enemigo2 enemigo2 = collision.gameObject.GetComponent<Enemigo2>();
        HandleEnemyCollision(enemigo2, collision, 2); // Tipo 2 para Enemigo2
    }
}

private void HandleEnemyCollision(object enemigo, Collision2D collision, int type)
{
    // Desactiva el enemigo y lo devuelve al pool con su tipo
    enemySpawner.ReturnEnemyToPool(collision.gameObject, type);

    // Genera la explosión en la posición del enemigo
    SpawnExplosion(collision.transform.position);

    // Agrega puntos al contador según el tipo de enemigo
    if (enemigo is Enemigo)
    {
        ((Enemigo)enemigo).OnBulletCollision();
    }
    else if (enemigo is Enemigo2)
    {
        ((Enemigo2)enemigo).OnBulletCollision();
    }

    // Devuelve la bala al pool
    bulletPool.ReturnToPool(gameObject);
}
    void SpawnExplosion(Vector3 position)
    {
        GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        Destroy(explosion, lifetime); // explosion se destruirá, pero el prefab sigue intacto
    }
}