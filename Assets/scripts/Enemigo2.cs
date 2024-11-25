using UnityEngine;

public class Enemigo2 : MonoBehaviour
{
    public float speed = 5f;  // Velocidad del enemigo
    private Vector2 direction;  // Dirección del movimiento
    public float lifetime = 10f; // Tiempo antes de regresar al pool automáticamente
    private float timer;
    public int points = 10;  // Puntos que da este enemigo al ser destruido
    private Vector2 minBounds; // Límites inferiores de la pantalla
    private Vector2 maxBounds; // Límites superiores de la pantalla
    private float halfWidth;
    private float halfHeight;

    void OnEnable()
    {
        timer = 0f; // Reinicia el temporizador al activarse
        SetInitialDirection();

        // Calcula los límites de la pantalla en coordenadas del mundo
        Camera cam = Camera.main;
        minBounds = cam.ScreenToWorldPoint(Vector3.zero);
        maxBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        // Calcula la mitad del tamaño del sprite
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        halfWidth = spriteRenderer.bounds.extents.x;
        halfHeight = spriteRenderer.bounds.extents.y;
    }

    void Update()
    {
        // Movimiento continuo
        transform.Translate(direction * speed * Time.deltaTime);

        // Rebota en los límites de la pantalla
        CheckBoundsAndBounce();

        // Control del tiempo de vida
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            ReturnToPool();
        }
    }

void OnCollisionEnter2D(Collision2D collision)
{
    // Lista de etiquetas a verificar
    string[] enemyTags = { "Enemigo", "Enemigo2", "JefeFinal"};

    if (System.Array.Exists(enemyTags, tag => tag == collision.gameObject.tag))
    {
        Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
    }
}

    private void SetInitialDirection()
    {
        // Establece una dirección inicial aleatoria
        float randomAngle = Random.Range(0f, 360f);
        direction = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
    }

    private void CheckBoundsAndBounce()
    {
        Vector3 position = transform.position;

        // Rebota en los bordes horizontales
        if (position.x - halfWidth < minBounds.x || position.x + halfWidth > maxBounds.x)
        {
            direction.x = -direction.x;
            position.x = Mathf.Clamp(position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        }

        // Rebota en los bordes verticales
        if (position.y - halfHeight < minBounds.y || position.y + halfHeight > maxBounds.y)
        {
            direction.y = -direction.y;
            position.y = Mathf.Clamp(position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
        }

        transform.position = position;
    }

    private void ReturnToPool()
    {
        EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
        if (spawner != null)
        {
            spawner.ReturnEnemyToPool(gameObject, 2);
        }
        else
        {
            Debug.LogError("No se encontró un EnemySpawner en la escena.");
        }
    }

    public void OnBulletCollision()
    {
        GameManager.Instance.AddScore(points);
    }
}

