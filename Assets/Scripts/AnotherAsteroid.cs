using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherAsteroid : MonoBehaviour, IDamageable
{
    //references
    private Rigidbody2D _rb;
    [SerializeField]private AnotherAsteroid _asteroidPrefab;

    //fields
    [Range(0,10)]private float _size = 1;
    private int _maxHealth, _health;
    private float Size { //this size also serves as the maxHealth
        get => _size;
        set 
        {
            _size = value;
            transform.localScale = new Vector3(_size, _size, 1);
            if (_rb != null) _rb.mass = _size;
        }
    }
    private Vector2 _velocity = Vector2.one;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        // TODO: unhardcode sizes and speed
        // TODO: unhardcode sizes, speed and health
        // a lot of things that are here should be refactor with how you will instantiate another asteroid prefabs in mind
        Init();
    }

    public void Init()
    {
        Size = Random.Range(0.5f, 2f);

        _velocity = Random.insideUnitCircle.normalized * Random.Range(2f, 5f);
        _rb.velocity = _velocity;

        _maxHealth = (int)Size;
        _health = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if(_health <= 0)return;
        _health -= damage;
        ReduceSize();

        if(_health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    void ReduceSize()
    {
        if (Size > 1) {
            Size--;

            Instantiate(gameObject);
        }else
        {
            Die();        
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        GameObject otherGO = other.gameObject;
        if(otherGO.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.TakeDamage(1);
        }
        Vector2 directionToBounce = _velocity - (Vector2)otherGO.transform.position + (-_velocity/2);
        Bounce(directionToBounce);
        TakeDamage(1);
    }

    private void Bounce(Vector2 bounceDir)
    {
        _rb.velocity = bounceDir.normalized * (_velocity.magnitude / 2);
    }
}
