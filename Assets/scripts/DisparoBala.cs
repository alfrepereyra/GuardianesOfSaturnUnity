using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public BulletPool bulletPool; 
    public Transform firePoint;    //punto desde el cual dispara la bala
    public float bulletSpeed = 10f; //Velocidad
    public AudioClip shootSound;   
    private AudioSource audioSource;

    public ExplosionPool explosionPool; //pool de explosiones
    
    void Start()
    {
       
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {  //dispara la bala
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        //obtiene una bala del pool
        GameObject bullet = bulletPool.GetBullet(firePoint.position, firePoint.rotation);
        //mueve la bala en la dirección del disparo
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = firePoint.up * bulletSpeed;

        if (audioSource && shootSound)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }


}
