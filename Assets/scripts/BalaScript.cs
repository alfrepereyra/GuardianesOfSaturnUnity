using UnityEngine;

public class BalaScript : MonoBehaviour
{
    private BulletPool bulletPool; //pool de balas utilizado en el juego
    private EnemySpawner enemySpawner; //controlador para el manejo de enemigos
    public GameObject explosionPrefab; // prefab para generar explosiones
    public float lifetime = 0.5f;

    void Start()
    {
        Collider2D balaCollider = GetComponent<Collider2D>();

        //ignora las colisiones con las balas del jefe
        GameObject[] balasContrarias = GameObject.FindGameObjectsWithTag("BalaJefe");
        foreach (GameObject bala in balasContrarias)
        {
            Collider2D otroCollider = bala.GetComponent<Collider2D>();
            if (otroCollider != null)
            {
                Physics2D.IgnoreCollision(balaCollider, otroCollider);
            }
        }
    }

    private void Awake()
    {//referencias a el pool de balas y enemigo
        bulletPool = FindObjectOfType<BulletPool>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            Enemigo enemigo = collision.gameObject.GetComponent<Enemigo>();
            HandleEnemyCollision(enemigo, collision, 1); //tipo 1 para enemigo estándar
        }
        else if (collision.gameObject.CompareTag("Enemigo2"))
        {
            Enemigo2 enemigo2 = collision.gameObject.GetComponent<Enemigo2>();
            HandleEnemyCollision(enemigo2, collision, 2); //tipo 2 para enemigo alternativo
        }
        else if (collision.gameObject.CompareTag("JefeFinal"))
        {
            JefeFinal jefeFinal = collision.gameObject.GetComponent<JefeFinal>();
            HandleBossCollision(jefeFinal, collision);
        }
        else if (collision.gameObject.CompareTag("BalaJefe"))
        {
            //evita colisiones entre la bala del jugador y las del jefe
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }

    //maneja las colisiones con los enemigos 1 y 2
    private void HandleEnemyCollision(object enemigo, Collision2D collision, int type)
    {
        enemySpawner.ReturnEnemyToPool(collision.gameObject, type);
        SpawnExplosion(collision.transform.position);
        if (enemigo is Enemigo)
        {
            ((Enemigo)enemigo).OnBulletCollision();
        }
        else if (enemigo is Enemigo2)
        {
            ((Enemigo2)enemigo).OnBulletCollision();
        }
        bulletPool.ReturnToPool(gameObject);
    }

    //maneja las colisiones con el boss
    private void HandleBossCollision(JefeFinal jefeFinal, Collision2D collision)
    {
        if (jefeFinal != null)
        {
            int bulletDamage = 1;
            jefeFinal.TakeDamage(bulletDamage);

            if (jefeFinal.IsDead())
            {
                //genera una explosión en la posición del jefe
                SpawnExplosion(collision.transform.position);
                GameManager.Instance.AddScore(jefeFinal.points);
                Destroy(jefeFinal.gameObject);
            }
        }

        //regresa la bala al pool
        bulletPool.ReturnToPool(gameObject);
    }
    
    //explosion
    void SpawnExplosion(Vector3 position)
    {
        GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        Destroy(explosion, lifetime); // explosion se destruirá, pero el prefab sigue intacto
    }
}
