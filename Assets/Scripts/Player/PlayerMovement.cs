using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float thrust = 1, rotationSpeed = 1, maxVelocity = 1;

    private Vector2 movement;
    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate() {
        Move();
        Rotate();
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
    }

    
}