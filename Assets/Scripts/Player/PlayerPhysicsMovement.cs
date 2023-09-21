using UnityEngine;

public class PlayerPhysicsMovement : MonoBehaviour
{
    [SerializeField] private float thrust = 1, rotationSpeed = 1, bulletImpulse = 1, maxVelocity = 1;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;

    private Vector2 movement;
    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (Input.GetButtonUp("Fire1")) Shoot();
    }

    private void FixedUpdate() {
        Move();
        Rotate();
    }

    private Quaternion CalculateRotation() {
        var rotationAngle = Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, rotationAngle);
    }

    private void Move() {
        if (movement.y < 0) {
            rb2d.velocity = Vector2.MoveTowards(rb2d.velocity, Vector2.zero, thrust * Time.fixedDeltaTime);
            rb2d.angularVelocity = Mathf.MoveTowards(rb2d.angularVelocity, 0, rotationSpeed * Time.fixedDeltaTime);
        }
        else {
            rb2d.AddForce(transform.up * movement.y * thrust);
            rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, maxVelocity);
        }
    }

    private void Rotate() {
        rb2d.rotation -= movement.x * rotationSpeed * Time.fixedDeltaTime;
        if (movement.x == 0) {
            rb2d.angularVelocity = Mathf.MoveTowards(rb2d.angularVelocity, 0, rotationSpeed * 2 * Time.fixedDeltaTime);
        }
        else {
            rb2d.angularVelocity -= movement.x * rotationSpeed * Time.fixedDeltaTime;
        }
    }

    private void Shoot() {
        var rotation = CalculateRotation();
        GameObject b = Instantiate(bulletPrefab, shootPoint.position, rotation);
        b.GetComponent<FatBullet>().Fire(transform.up, bulletImpulse, 1);
        rb2d.AddForce(-transform.up * bulletImpulse, ForceMode2D.Impulse);
    }
}