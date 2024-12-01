using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPool : MonoBehaviour
{
    public GameObject explosionPrefab; //prefab de la explosión
    public int poolSize = 10;          
    public float explosionLifetime = 1.5f; 

    private Queue<GameObject> explosionPool = new Queue<GameObject>();
    private GameObject[] explosionsPool;
    
    void Start()
    {
        //inicia el pool de explosiones
        for (int i = 0; i < poolSize; i++)
        {
            GameObject explosion = Instantiate(explosionPrefab);
            explosion.SetActive(false);
            explosionPool.Enqueue(explosion);
        }
    }

    //obtiene una explosion del pool
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (!explosionsPool[i].activeInHierarchy)
            {
                return explosionsPool[i];  
            }
        }
        return null;  //Si no hay explosiones disponibles
    }


    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        explosionPool.Enqueue(obj); 
    }


//desactiva la explosion
    public IEnumerator DeactivateAfterTime(GameObject obj)
    {
        yield return new WaitForSeconds(explosionLifetime);
        ReturnToPool(obj);
    }
}
