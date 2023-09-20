using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidPooler : MonoBehaviour
{
    Dictionary<GameObject, AnotherAsteroid> pooledAsteroids = new();
    [SerializeField] List<GameObject> pooledObjects;
    [SerializeField] GameObject objectToPool;
    [SerializeField] int amountToPool;
    [SerializeField]bool resizeable = true;
    [SerializeField] Transform objSpawnPos;
    

    void Start()
    {
        pooledObjects = new List<GameObject>();
        for(int i = 0;i<amountToPool;i++)
        {
            CreateNewObject();
        }
    }

    public KeyValuePair<GameObject, AnotherAsteroid> GetPooledObject()
    {
        foreach(var obj in pooledAsteroids)
        {
            if(!obj.Key.activeInHierarchy)
            {
                obj.Key.SetActive(true);
                return obj;
            }
        }
        if(resizeable)return CreateNewObject();
        return new KeyValuePair<GameObject,AnotherAsteroid>(null, null);
    }

    public KeyValuePair<GameObject, AnotherAsteroid> GetObjectAnyway()
    {
        foreach(var obj in pooledAsteroids)
        {
            if(!obj.Key.activeInHierarchy)
            {
                obj.Key.SetActive(true);
                return obj;
            }
        }
        return CreateNewObject();
    }

    private KeyValuePair<GameObject, AnotherAsteroid> CreateNewObject()
    {
        GameObject newObject = Instantiate(objectToPool);
        if(objSpawnPos != null) newObject.transform.SetParent(objSpawnPos.transform);
        else  newObject.transform.SetParent(this.transform);
        newObject.SetActive(false);

        AnotherAsteroid asteroidScript;
        if(newObject.TryGetComponent<AnotherAsteroid>(out asteroidScript))
        {
            KeyValuePair<GameObject,AnotherAsteroid> asteroid = new(newObject, asteroidScript);
            pooledAsteroids.Add(asteroid.Key, asteroid.Value);
            return asteroid;

        }else
        {
            Debug.LogError("The object in the asteroid pooler: " + name + " does not have a valid Asteroid Script");
            return new KeyValuePair<GameObject,AnotherAsteroid>(null, null);
        }




    }
}