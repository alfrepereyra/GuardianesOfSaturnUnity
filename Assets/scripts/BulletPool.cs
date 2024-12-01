using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public int poolSize = 20; // tamaño del pool 
    public float bulletLifetime = 5f; //tiempo de vida de cada bala 

    private Queue<GameObject> bulletPool = new Queue<GameObject>(); // cola para gestionar el pool de balas

    public ExplosionPool explosionPool; //referencia al ExplosionPool para generar explosiones

    void Start()
    {
        //inicia el pool con balas inactivas
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }

    public GameObject GetBullet(Vector3 position, Quaternion rotation)
    {
        if (bulletPool.Count > 0)
        {
            GameObject bullet = bulletPool.Dequeue();
            bullet.transform.position = position;
            bullet.transform.rotation = rotation;
            bullet.SetActive(true);

            // inicia el tiempo de vida de la bala
            StartCoroutine(DeactivateBulletAfterTime(bullet));

            return bullet;
        }
        else
        {
            //si no hay balas en el pool, crea una nueva
            GameObject bullet = Instantiate(bulletPrefab, position, rotation);
            StartCoroutine(DeactivateBulletAfterTime(bullet));
            return bullet;
        }
    }

    private IEnumerator DeactivateBulletAfterTime(GameObject bullet)
    {
        yield return new WaitForSeconds(bulletLifetime);

        // desactiva la bala y la pone de vuelta en el pool
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }

    public void ReturnToPool(GameObject bullet)
    {
        bullet.SetActive(false); 
        bulletPool.Enqueue(bullet); 
    }
}
