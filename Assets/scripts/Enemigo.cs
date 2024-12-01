using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float lifetime = 10f; //tiempo antes de regresar al pool automaticamente
    private float timer;
    public int puntosPorEnemigo = 10; 


    void OnEnable()
    {
        timer = 0f; //reinicia el temporizador al activarse
    }

    void Update()
    {
        timer += Time.deltaTime;
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
        // Pasamos el valor 1 para el tipo Enemigo
        spawner.ReturnEnemyToPool(gameObject, 1);
    }
    else
    {
        Debug.LogError("No se encontró un EnemySpawner en la escena.");
    }
}
void OnCollisionEnter2D(Collision2D collision)
{
    //lista de enemigos a verificar
    string[] enemyTags = { "Enemigo", "Enemigo2", "JefeFinal"};

    if (System.Array.Exists(enemyTags, tag => tag == collision.gameObject.tag))
    {
        Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
    }
}

public void OnBulletCollision()
{
    //agrega puntos al GameManager
        GameManager.Instance.AddScore(puntosPorEnemigo);

}
}
