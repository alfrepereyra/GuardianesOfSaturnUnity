using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public GameObject bulletPrefab;  // Prefab de la bala
    public int poolSize = 20;       // Tamaño del pool de balas
    public float bulletLifetime = 5f; // Tiempo de vida de cada bala en segundos

    private Queue<GameObject> bulletPool = new Queue<GameObject>();  // Cola para gestionar el pool de balas

    // Referencia al ExplosionPool para generar explosiones
    public ExplosionPool explosionPool;

    void Start()
    {
        // Inicializa el pool con balas inactivas
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }

    // Método para obtener una bala del pool
    public GameObject GetBullet(Vector3 position, Quaternion rotation)
    {
        if (bulletPool.Count > 0)
        {
            GameObject bullet = bulletPool.Dequeue();
            bullet.transform.position = position;
            bullet.transform.rotation = rotation;
            bullet.SetActive(true);

            // Inicia el tiempo de vida de la bala
            StartCoroutine(DeactivateBulletAfterTime(bullet));

            return bullet;
        }
        else
        {
            // Si no hay balas disponibles en el pool, crea una nueva
            GameObject bullet = Instantiate(bulletPrefab, position, rotation);
            StartCoroutine(DeactivateBulletAfterTime(bullet));
            return bullet;
        }
    }

    // Desactiva la bala después de un tiempo
    private IEnumerator DeactivateBulletAfterTime(GameObject bullet)
    {
        yield return new WaitForSeconds(bulletLifetime);

        // Desactiva la bala y la pone de vuelta en el pool
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }


    // Método para devolver la bala al pool
    public void ReturnToPool(GameObject bullet)
    {
        bullet.SetActive(false);  // Desactiva la bala
        bulletPool.Enqueue(bullet);  // La agrega al pool
    }
}
