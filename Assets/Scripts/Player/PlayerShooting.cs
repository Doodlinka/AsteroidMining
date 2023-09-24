using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private const int GRAPPLEMASK = 1 << 6;


    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float bulletImpulse = 1, grappleRange = 10, grappleSpeed = 8;
    [SerializeField] private Transform beamView;
    [SerializeField] private AudioClip[] shootSounds;
    [SerializeField] private AudioClip grappleSound;
    private AudioSource audioSource;
    private Rigidbody2D rb2d; 


    private void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButtonDown("A")) {
            Shoot();
            audioSource.PlayOneShot(shootSounds[Random.Range(0, shootSounds.Length)]);
        }
        if (Input.GetButton("B")) Grapple();
        if (Input.GetButtonDown("B")) {
            audioSource.clip = grappleSound;
            audioSource.Play();
            beamView.gameObject.SetActive(true);
        }
        if (Input.GetButtonUp("B")) {
            beamView.gameObject.SetActive(false);
            audioSource.Stop();
        }
    }

    private void Shoot()
    {
        GameObject b = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        b.GetComponent<Bullet>().Fire(transform.up, bulletImpulse, 1);
        rb2d.AddForce(-transform.up * bulletImpulse, ForceMode2D.Impulse);
    }

    private void Grapple() {
        // RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, grappleRange, GRAPPLEMASK);
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new(0.25f, 0.25f), 0, transform.up, grappleRange, GRAPPLEMASK);
        if (hit.rigidbody) {
            rb2d.AddForce(transform.up * grappleSpeed);
            hit.rigidbody.AddForce(-transform.up * grappleSpeed);
            SetBeamView(hit.point, 0.25f);
            // rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, grappleSpeed);
            // hit.rigidbody.velocity = Vector2.ClampMagnitude(hit.rigidbody.velocity, grappleSpeed);
            // rb2d.velocity = transform.up * grappleSpeed / rb2d.mass;
            // hit.rigidbody.velocity = -transform.up * grappleSpeed / hit.rigidbody.mass;
        }
        else {
            SetBeamView(transform.position + transform.up * grappleRange, 0.0625f);
        }
    }

    private void SetBeamView(Vector3 target, float width) {
        beamView.position = (shootPoint.position + target) / 2;
        beamView.rotation.SetFromToRotation(shootPoint.position, target);
        beamView.localScale = new(width, (target - shootPoint.position).magnitude, 0);
    }

    // private Quaternion RotationBetween(Vector2 a, Vector2 b) 
    // {
    //     var rotationAngle = Mathf.Atan2(b.y - a.y, b.x - a.x) * Mathf.Rad2Deg;
    //     return Quaternion.Euler(0, 0, rotationAngle);
    // }
}
