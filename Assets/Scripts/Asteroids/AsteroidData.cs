using UnityEngine;

public class AsteroidData
{
    //references
    private Transform _asteroidTransform;
    private Rigidbody2D _rb;

    //fields
    [Range(0,10)]private float _size = 1f;
    public int MaxHealth = 2, Health;
    private int _divisions = Random.Range(2,4);
    private float _speedMultiplier = Random.Range(1.5f,5f);
    private Vector2 _velocity = Vector2.zero;

    //properties
    public int DivisionsCount => _divisions;
    public Rigidbody2D rigidbody => _rb;
    public Vector3 position => _asteroidTransform.position;
    public float Speed => _speedMultiplier;
    public Vector2 Velocity
    {
        get => _velocity;
        set
        {
            _velocity = value;
            if(_rb != null)_rb.velocity = _velocity;
        }
    }
    public float Size
    { 
        get => _size;
        set 
        {
            _size = value;
            if(_asteroidTransform != null) _asteroidTransform.localScale = new Vector3(_size, _size, 1);
            if (_rb != null) _rb.mass = _size;
        }
    }


    public AsteroidData(float size, int maxHealth, Rigidbody2D rigidbody2D, Vector2 velocity)
    {
        Size = size;
        MaxHealth = maxHealth;
        _rb = rigidbody2D;
        Velocity = velocity;
        Health = MaxHealth;
        if(_rb != null)_asteroidTransform = _rb.transform;
    }

}
