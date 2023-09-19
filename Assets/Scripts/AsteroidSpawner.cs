using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AsteroidPooler))]
public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField]Transform[] _spawnPoints;
    [SerializeField]AsteroidPooler _pool;
    AsteroidData _initialValues;

    [SerializeField]float _spawnCooldown = 0.5f;
    float _nextSpawnTime;

    bool _alreadySpawnedTest = false;

    private void Awake() {
        _nextSpawnTime = _spawnCooldown;
        _pool = GetComponent<AsteroidPooler>();

        if(_initialValues == null)
        {
            SetStartingAsteroidValues();
        }
        
    }

    void SetStartingAsteroidValues()
    {
        float size = Random.Range(1.5f, 4.5f);
        int maxHealth = Random.Range(2, 4);
        Vector2 velocity = Random.insideUnitCircle.normalized * Random.Range(2.5f, 4.5f);
        _initialValues = new(size, maxHealth, null, velocity);

    }

    // Start is called before the first frame update
    void Start()
    {
        //this is just for testing, im gonna use a timer in the update method
        InvokeRepeating("Spawn", 1, _spawnCooldown);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        if(_alreadySpawnedTest)return; 
        //so we are going to assign the createCopy method to each asteroid
        KeyValuePair<GameObject, AnotherAsteroid> asteroid = _pool.GetPooledObject();
        if(asteroid.Key == null || asteroid.Value == null) return;

        asteroid.Key.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;

        var asteroidData = asteroid.Value.data;
        asteroidData.MaxHealth = _initialValues.MaxHealth;
        asteroidData.Health = _initialValues.Health;
        asteroidData.Size = _initialValues.Size;
        asteroidData.Velocity = _initialValues.Velocity;

        if(!asteroid.Value.SubscribedToEvent)
        {
            //checking the event subscription with a boolean is so ugly it hurts
            asteroid.Value.SubscribedToEvent = true;
            asteroid.Value.onSplit += CreateCopy;
        }
        _alreadySpawnedTest = true;
    }

    void CreateCopy(AsteroidData data)
    {
        Debug.Log("Creating " + data.DivisionsCount + " copies of asteroids");
        //here you should do a for loop with the number of divisions of the data
        for(int i = 0; i < data.DivisionsCount; i++)
        {
            KeyValuePair<GameObject, AnotherAsteroid> asteroid = _pool.GetObjectAnyway();
            if(asteroid.Key == null || asteroid.Value == null) return;
            asteroid.Key.transform.position = data.position;

            int oppositeDirection = (i % 2 == 0) ? 1 : -1; //im going to use this integer so i can make each asteroid division "bounce" in oppositeDirections
            var asteroidData = asteroid.Value.data;
            asteroidData.Velocity = data.Velocity * Vector2.right * oppositeDirection;
            asteroidData.Health = _initialValues.MaxHealth; //here im resetting the health but i dont know if its okay
            asteroidData.Size = _initialValues.Size;

        }

    }
}
