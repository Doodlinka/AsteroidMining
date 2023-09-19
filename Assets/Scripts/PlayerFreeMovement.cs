using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerFreeMovement : MonoBehaviour
{

    [SerializeField]
    private float _moveSpeed = 1;
    [SerializeField]
    private float _rotationSpeed = 1;

    private Vector2 _movement;
    private Rigidbody2D _rb;

    private void Awake() 
    {
        if(_rb == null) _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate()
    {
        Move();
        RotateShip();
    }

    private void RotateShip()
    {
        var rotationAmount = _movement.x * _rotationSpeed * Time.deltaTime;
        _rb.rotation -= rotationAmount;
    }

    private void Move()
    {
        Vector2 direction = transform.up * _movement.y;
        _rb.AddForce(direction * _moveSpeed, ForceMode2D.Force);
    }
}
