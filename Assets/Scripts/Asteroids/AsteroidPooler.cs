// TODO: delete this thing and maybe replace it with one of Unity's pools
// using System.Collections.Generic;
// using UnityEngine;

// public class AsteroidPooler : MonoBehaviour
// {
//     Dictionary<GameObject, Asteroid> pooledAsteroids = new();
//     [SerializeField] List<GameObject> pooledObjects;
//     [SerializeField] GameObject objectToPool;
//     [SerializeField] int amountToPool;
//     [SerializeField]bool resizeable = true;
//     [SerializeField] Transform objSpawnPos;
    

//     void Start()
//     {
//         pooledObjects = new List<GameObject>(amountToPool);
//         for(int i = 0; i < amountToPool; i++)
//         {
//             CreateNewObject();
//         }
//     }

//     public KeyValuePair<GameObject, Asteroid> GetPooledObject(bool forced = false)
//     {
//         foreach(var obj in pooledAsteroids)
//         {
//             if(!obj.Key.activeInHierarchy)
//             {
//                 obj.Key.SetActive(true);
//                 return obj;
//             }
//         }
//         if(forced || resizeable) return CreateNewObject();
//         return new KeyValuePair<GameObject,Asteroid>(null, null);
//     }

//     private KeyValuePair<GameObject, Asteroid> CreateNewObject()
//     {
//         GameObject newObject = Instantiate(objectToPool);
//         if(objSpawnPos != null) newObject.transform.SetParent(objSpawnPos.transform);
//         else  newObject.transform.SetParent(this.transform);
//         newObject.SetActive(false);

//         Asteroid asteroidScript;
//         if(newObject.TryGetComponent<Asteroid>(out asteroidScript))
//         {
//             KeyValuePair<GameObject,Asteroid> asteroid = new(newObject, asteroidScript);
//             pooledAsteroids.Add(asteroid.Key, asteroid.Value);
//             return asteroid;

//         }else
//         {
//             Debug.LogError("The object in the asteroid pooler: " + name + " does not have a valid Asteroid Script");
//             return new KeyValuePair<GameObject,Asteroid>(null, null);
//         }




//     }
// }