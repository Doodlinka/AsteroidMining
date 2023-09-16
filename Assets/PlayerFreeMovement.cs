using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeMovement : MonoBehaviour
{

    //references
    Rigidbody2D _rb;

    //fields
    [SerializeField]private float _moveSpeed = 1;
    [SerializeField]private float _rotationSpeed = 1;
    Vector2 _movement;

    private void Awake() {
        if(_rb == null) _rb = GetComponent<Rigidbody2D>();
    }

    

    // Update is called once per frame
    void Update()
    {
        _movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
    }

    private void FixedUpdate() 
    {
        if(_movement.sqrMagnitude > 0.1f)
        {
            Move();
        }else
        {
            Iddle();
        }
    }

    void Move()
    {
        //rotation
        float angle = Mathf.Atan2(_movement.y, _movement.x) * Mathf.Rad2Deg - 90;
        Vector3 newRotation = transform.rotation.eulerAngles;
        newRotation.z = angle;

        transform.rotation = Quaternion.Euler(newRotation);

        //movement
        float signOfAngle = Mathf.Sign(angle);
        _movement.Normalize();
        Vector2 direction = transform.up * signOfAngle * _movement.y;

        _rb.MovePosition(_rb.position + direction * (_moveSpeed * Time.fixedDeltaTime));
        

        //transform.up = direction;


    }

    void Iddle()
    {

    }
}
