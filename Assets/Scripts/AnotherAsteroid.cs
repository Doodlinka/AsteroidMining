using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherAsteroid : MonoBehaviour, IDamageable
{
    //references
    private Rigidbody2D _rb;


    //fields
    private float _size = 1;
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
        Size = Random.Range(1f, 2.25f);
        _velocity = Random.insideUnitCircle.normalized * Random.Range(3.3f, 8f);
        _rb.velocity = _velocity;
        _maxHealth = (int)Size * 2;
        _health = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if(_health <= 0)return;
        _health -= damage;
        if(_health <= 0)
        {
            Die();
        }
    }

    public void Die() {
        if (Size > 1) {
            Size--;

            Destroy(gameObject);
        }
        else {
            Destroy(gameObject);
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
