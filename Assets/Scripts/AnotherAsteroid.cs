using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AnotherAsteroid : MonoBehaviour, IDamageable
{
    //references
    Rigidbody2D _rb;
    AsteroidData _data;

    //things 
    public event Action<AsteroidData> onSplit;
    [HideInInspector]public bool SubscribedToEvent = false;

    //properties
    public AsteroidData data => _data;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _data = new(1, 2, _rb, Vector2.zero);

    }

    public void TakeDamage(int damage)
    {
        if(_data.Health <= 0)return;
        _data.Health -= damage;
        Debug.Log(name + ": current health " + _data.Health);
        if(_data.Health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if(_data.Size > 1.25f)
        {
            _data.Size--;
            onSplit?.Invoke(_data);
            gameObject.SetActive(false);
        }else
        {
            gameObject.SetActive(false);
        }

        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        GameObject otherGO = other.gameObject;
        if(otherGO.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.TakeDamage(1);
        }

        Vector2 directionToBounce = _data.Velocity - (Vector2)otherGO.transform.position;
        _data.Velocity = directionToBounce.normalized * _data.Speed;
        TakeDamage(1);
        //the lines above change the data of the asteroid (making it smaller, and slower? or at least changing its velocity to the opposite direction)

    }
}
