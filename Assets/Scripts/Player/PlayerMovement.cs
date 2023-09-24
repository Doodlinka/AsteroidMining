using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float thrust = 1, rotationSpeed = 1, maxVelocity = 1;
    [SerializeField] private Vector3 bottomLeft, topRight;
    // [SerializeField] private AudioClip[] thrustSounds;

    private Vector2 movement;
    // private bool wasMoving;
    private Rigidbody2D rb2d;
    // private AudioSource audioSource;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        // audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        // if (movement.sqrMagnitude > 0.01f && !wasMoving) {
        //     audioSource.clip = thrustSounds[Random.Range(0, thrustSounds.Length)];
        //     audioSource.Play();
        // }
        // else if (movement.sqrMagnitude <= 0.01f && wasMoving) {
        //     audioSource.Stop();
        // }
        // wasMoving = movement.sqrMagnitude <= 0.01f;
    }

    private void FixedUpdate() {
        if (movement.y < 0) {
            rb2d.velocity = Vector2.MoveTowards(rb2d.velocity, Vector2.zero, thrust * Time.fixedDeltaTime);
            // rb2d.angularVelocity = Mathf.MoveTowards(rb2d.angularVelocity, 0, rotationSpeed * Time.fixedDeltaTime);
        }
        else {
            rb2d.AddForce(transform.up * movement.y * thrust);
            rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, maxVelocity);
        }

        rb2d.rotation -= movement.x * rotationSpeed * Time.fixedDeltaTime;

        // restrict to the playable area
        transform.position = Vector3.Min(Vector3.Max(transform.position, bottomLeft), topRight);
    }
}