using UnityEngine;
using UnityEngine.SceneManagement;

public class JefeFinal : MonoBehaviour
{
    public float speed = 3f; // velocidad
    private Vector2 direction;
    private Vector2 minBounds;
    private Vector2 maxBounds;
    private float halfWidth;
    private float halfHeight;

    public int maxHealth = 30; // vida máxima del jefe
    private int currentHealth;

    public int points = 500; // puntos al ser destruido

    public GameObject BalaJefePrefab;

    public AudioClip sonidoDañoJefe; // Sonido al recibir daño
    private AudioSource audioSource;

    public Transform PuntoDisparo;
    public float fireRate = 0.5f; // intervalo entre disparos
    public float bulletSpeed = 5f; // velocidad de las balas

    public GameObject pantallaGanar;

    private float nextFireTime;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Salud inicial
        currentHealth = maxHealth;

        // Calcula los límites de la pantalla
        Camera cam = Camera.main;
        minBounds = cam.ScreenToWorldPoint(Vector3.zero);
        maxBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        // Calcula la mitad del tamaño del sprite
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        halfWidth = spriteRenderer.bounds.extents.x;
        halfHeight = spriteRenderer.bounds.extents.y;

        SetInitialDirection();

        // Asegúrate de que la pantalla de ganar esté inicialmente desactivada
        if (pantallaGanar != null)
        {
            pantallaGanar.SetActive(false);
        }

    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        CheckBoundsAndBounce();

        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void SetInitialDirection()
    {
        float randomAngle = Random.Range(0f, 360f);
        direction = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
    }

    private void CheckBoundsAndBounce()
    {
        Vector3 position = transform.position;

        // Rebote en los bordes horizontales
        if (position.x - halfWidth < minBounds.x || position.x + halfWidth > maxBounds.x)
        {
            direction.x = -direction.x;
            position.x = Mathf.Clamp(position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        }

        // Rebote en los bordes verticales
        if (position.y - halfHeight < minBounds.y || position.y + halfHeight > maxBounds.y)
        {
            direction.y = -direction.y;
            position.y = Mathf.Clamp(position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
        }

        transform.position = position;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Reproduce el sonido al recibir daño
        if (audioSource != null && sonidoDañoJefe != null)
        {
            audioSource.PlayOneShot(sonidoDañoJefe);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }

    private void Die()
    {
        // Añade puntos al GameManager
        GameManager.Instance.AddScore(points);

        // Mostrar la pantalla de ganar
        if (pantallaGanar != null)
        {
            pantallaGanar.SetActive(true);
        }

        Destroy(gameObject);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BalaNave"))
        {
            int damage = 1;
            TakeDamage(damage);
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string[] enemyTags = { "Enemigo", "Enemigo2", "JefeFinal" };
        if (System.Array.Exists(enemyTags, tag => tag == collision.gameObject.tag))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(BalaJefePrefab, PuntoDisparo.position, Quaternion.identity);

        // Generar una dirección aleatoria en el rango [-1, 1] para X e Y
        Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = randomDirection * bulletSpeed;
    }
}
