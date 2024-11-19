using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPool : MonoBehaviour
{
    public GameObject explosionPrefab; // Prefab de la explosión
    public int poolSize = 10;          // Tamaño del pool de explosiones
    public float explosionLifetime = 1.5f; // Duración de la animación de la explosión

    private Queue<GameObject> explosionPool = new Queue<GameObject>();
    private GameObject[] explosionsPool;
    
    void Start()
    {
        // Inicializa el pool de explosiones con objetos inactivos
        for (int i = 0; i < poolSize; i++)
        {
            GameObject explosion = Instantiate(explosionPrefab);
            explosion.SetActive(false);
            explosionPool.Enqueue(explosion);
        }
    }

    /// <summary>
    /// Obtiene una explosión del pool.
    /// </summary>
    public GameObject GetPooledObject()
    {
        // Devuelve una explosión del pool si está disponible
        for (int i = 0; i < poolSize; i++)
        {
            if (!explosionsPool[i].activeInHierarchy)
            {
                return explosionsPool[i];  // Devuelve la explosión inactiva
            }
        }
        return null;  // Si no hay explosiones disponibles
    }
    /// <summary>
    /// Devuelve un objeto al pool.
    /// </summary>
    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        explosionPool.Enqueue(obj); // Usa el nombre correcto
    }

    /// <summary>
    /// Desactiva una explosión después de un tiempo y la devuelve al pool.
    /// </summary>
    public IEnumerator DeactivateAfterTime(GameObject obj)
    {
        yield return new WaitForSeconds(explosionLifetime);
        ReturnToPool(obj);
    }
}
