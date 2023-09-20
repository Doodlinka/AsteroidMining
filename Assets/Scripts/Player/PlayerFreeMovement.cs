using UnityEngine;

public class PlayerFreeMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float rotationSpeed = 1;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;

    private Vector2 _movement;
    private Rigidbody2D _rb;

    private void Awake()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (Input.GetButtonUp("Fire1")) Shoot();
    }

    private void FixedUpdate()
    {
        Move();
        RotateShip();
    }

    private Quaternion CalculateRotation()
    {
        var rotationAngle = Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, rotationAngle);
    }

    private void Move()
    {
        Vector2 direction = transform.up * _movement.y;
        _rb.AddForce(direction * moveSpeed, ForceMode2D.Force);
    }

    private void RotateShip()
    {
        var rotationAmount = _movement.x * rotationSpeed * Time.deltaTime;
        _rb.rotation -= rotationAmount;
    }

    private void Shoot()
    {
        var rotation = CalculateRotation();
        Instantiate(bulletPrefab, shootPoint.position, rotation);
    }
}