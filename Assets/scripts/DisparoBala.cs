using UnityEngine;
 // Asigna aquí el prefab de la explosión en el Inspector


public class PlayerShooting : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject bulletPrefab;   // Prefab de la bala
    public Transform firePoint;       // Punto desde el cual dispara la bala
    public float bulletSpeed = 10f;   // Velocidad de la bala
    public AudioClip shootSound;      // Sonido de disparo
    private AudioSource audioSource;

    void Start()
    {
        // Obtiene el AudioSource para reproducir el sonido de disparo
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Dispara la bala al presionar la barra espaciadora
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Instancia la bala en el punto de disparo y le asigna una dirección
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * bulletSpeed; // Ajusta según la dirección que necesites (aquí es hacia arriba)

        // Reproduce el sonido de disparo
        if (audioSource && shootSound)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

     

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo")) // Asegúrate de que el enemigo tenga el tag "Enemigo"
        {
            // Instancia la explosión en la posición de la colisión
            Instantiate(explosionPrefab, collision.transform.position, Quaternion.identity);

            // Destruye solo la instancia específica del enemigo con el que colisiona
            Destroy(collision.gameObject);

            // Destruye la bala después de la colisión
            Destroy(bulletPrefab);
        }
    }
        
}
