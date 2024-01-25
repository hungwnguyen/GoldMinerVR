using System.Collections.Generic;
using UnityEngine;



public class ObjectPooler<T>
{
    public delegate GameObject SpawnerDelegate(T instance);
    public Dictionary<T, GameObject> poolDictionary;
    private IPooledObject pooledObject;
    public SpawnerDelegate OnSpawned;

    public ObjectPooler()
    {
        poolDictionary = new Dictionary<T, GameObject>();
    }

    public delegate void Spawner();
    public void SpawnFromPool(T instance)
    {
        if (!poolDictionary.ContainsKey(instance))
        {
            AddElement(instance, OnSpawned(instance));
        }
        else
        {
            GameObject objectToSpawn = poolDictionary[instance];
            objectToSpawn.SetActive(true);
            Spawn(objectToSpawn);
        }
    }
    
    private void Spawn(GameObject objectToSpawn)
    {
        pooledObject = objectToSpawn.GetComponent<IPooledObject>();
        if (pooledObject != null)
        {
            pooledObject.OnObjectSpawn();
        }
        else
        {
            Debug.LogWarning("pooled object is null!");
        }
    }

    public void AddElement(T param, GameObject GO, bool isSpawned = true)
    {
        poolDictionary.Add(param, GO);
        if (isSpawned)
        {
            Spawn(GO);
        }
        else
        {
            GO.SetActive(false);
        }
        
    }
}
