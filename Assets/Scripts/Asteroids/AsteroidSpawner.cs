using UnityEngine;

// [RequireComponent(typeof(AsteroidPooler))]
public class AsteroidSpawner : MonoBehaviour
{
    // [SerializeField] private AsteroidPooler _pool;
    [SerializeField] private GameObject _asteroidPrefab;

    [SerializeField] float _spawnCooldownMin = 0.5f, _spawnCooldownMax = 2f, _velocityMin = 2f, 
            _velocityMax = 6f, _spawnPosMargin = 3f, _oreChance = 0.25f;
    float _spawnTimer;

    private void Start() {
        _spawnTimer = Random.Range(_spawnCooldownMin, _spawnCooldownMax);
        // _pool = GetComponent<AsteroidPooler>();
    }

    void FixedUpdate()
    {
        _spawnTimer -= Time.fixedDeltaTime;
        if (_spawnTimer <= 0) {
            _spawnTimer += Random.Range(_spawnCooldownMin, _spawnCooldownMax);
            Spawn();
        }
    }

    private void Spawn() {
        var bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.farClipPlane));
        var topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.farClipPlane));
        Vector3 position = RandomPosOutsideRect(bottomLeft, topRight);
        Vector3 randomTarget = RandomPosInsideRect(bottomLeft, topRight);
        Vector2 velocity = (randomTarget - position).normalized * Random.Range(_velocityMin, _velocityMax);

        AsteroidData data = new(Random.Range(1, 4), position, velocity, Random.value <= _oreChance);
        Asteroid rock =  MakeAsteroid(data);

        // if (!asteroid.Value.SubscribedToEvent ){
            //checking the event subscription with a boolean is so ugly it hurts
            // asteroid.Value.SubscribedToEvent = true;
        rock.onSplit += CreateCopy;
        // }
       
    }

    private void CreateCopy(AsteroidData data) {
        // when an ore rock splits, the first piece is guaranteed to have ore
        // and handles it itself, the second one is twice as likely as the spawn ore chance
        data.HasOre &= Random.value <= 2 * _oreChance;
        MakeAsteroid(data);
    }

    private Vector3 RandomPosOutsideRect(Vector3 bottomLeft, Vector3 topRight) {
        var randomValue = Random.value;
        if (randomValue < 0.25f) return new (bottomLeft.x - _spawnPosMargin, Random.Range(bottomLeft.y, topRight.y), 0);
        else if (randomValue < 0.5f) return new (Random.Range(bottomLeft.x, topRight.x), topRight.y + _spawnPosMargin, 0);
        else if (randomValue < 0.75f) return new (topRight.x + _spawnPosMargin, Random.Range(bottomLeft.y, topRight.y), 0);
        else return new (Random.Range(bottomLeft.x, topRight.x), bottomLeft.y - _spawnPosMargin, 0);
    }

    private Vector3 RandomPosInsideRect(Vector3 bottomLeft, Vector3 topRight) {
        float x = Random.Range(bottomLeft.x + _spawnPosMargin, topRight.x - _spawnPosMargin);
        float y = Random.Range(bottomLeft.y + _spawnPosMargin, topRight.y - _spawnPosMargin);
        return new (x, y, 0);
    }

    private Asteroid MakeAsteroid(AsteroidData data) {
        // insert pool request here if we use a pool
        Asteroid rock = Instantiate(_asteroidPrefab).GetComponent<Asteroid>();
        rock.Init(data);
        return rock;
    }
}
