using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public BulletPool bulletPool;  // Referencia al BulletPool
    public Transform firePoint;    // Punto desde el cual dispara la bala
    public float bulletSpeed = 10f; // Velocidad de la bala
    public AudioClip shootSound;   // Sonido de disparo
    private AudioSource audioSource;

    public ExplosionPool explosionPool; // Referencia al pool de explosiones
    
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
        // Obtiene una bala del pool
        GameObject bullet = bulletPool.GetBullet(firePoint.position, firePoint.rotation);

        // Mueve la bala en la dirección del disparo
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = firePoint.up * bulletSpeed;  // Ajusta la dirección según el "up" del punto de disparo

        // Reproduce el sonido de disparo
        if (audioSource && shootSound)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }


}
