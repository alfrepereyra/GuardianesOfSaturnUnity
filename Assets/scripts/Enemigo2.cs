using UnityEngine;

public class Enemigo2 : MonoBehaviour
{
    public float speed = 5f;  //velocidad
    private Vector2 direction;  //direccion del movimiento
    public float lifetime = 10f; //tiempo antes de regresar al pool automáticamente
    private float timer;
    public int puntosPorEnemigo = 10;  
    private Vector2 minBounds; //limites inferiores de la pantalla
    private Vector2 maxBounds; //limites superiores de la pantalla
    private float halfWidth;
    private float halfHeight;

    void OnEnable()
    {
        timer = 0f; //reinicia el temporizador
        SetInitialDirection();

        //calcula los lmites de la pantalla
        Camera cam = Camera.main;
        minBounds = cam.ScreenToWorldPoint(Vector3.zero);
        maxBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        //calcula la mitad del tamaño del sprite
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        halfWidth = spriteRenderer.bounds.extents.x;
        halfHeight = spriteRenderer.bounds.extents.y;
    }

    void Update()
    {
        
        transform.Translate(direction * speed * Time.deltaTime);
        CheckBoundsAndBounce();

        //control del tiempo de vida
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            ReturnToPool();
        }
    }


    private void SetInitialDirection()
    {
        // Establece una dirección inicial aleatoria
        float randomAngle = Random.Range(0f, 360f);
        direction = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
    }


    //metodo para que reboten en los bordes de la pantalla
    private void CheckBoundsAndBounce()
    {
        Vector3 position = transform.position;
        if (position.x - halfWidth < minBounds.x || position.x + halfWidth > maxBounds.x)
        {
            direction.x = -direction.x;
            position.x = Mathf.Clamp(position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        }
        if (position.y - halfHeight < minBounds.y || position.y + halfHeight > maxBounds.y)
        {
            direction.y = -direction.y;
            position.y = Mathf.Clamp(position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
        }

        transform.position = position;
    }

    void OnCollisionEnter2D(Collision2D collision)
{
    //lista de enemigos
    string[] enemyTags = { "Enemigo", "Enemigo2", "JefeFinal"};

    if (System.Array.Exists(enemyTags, tag => tag == collision.gameObject.tag))
    {
        Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
    }
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
        Debug.LogError("no se encontro un EnemySpawner en la escena");
    }
}

    public void OnBulletCollision()
    {
        GameManager.Instance.AddScore(puntosPorEnemigo);
    }
}

