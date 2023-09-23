using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private const int GRAPPLEMASK = 1 << 6;


    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float bulletImpulse = 1, grappleRange = 10, grappleSpeed = 8;
    // [SerializeField] private Color beamColor;
    private Rigidbody2D rb2d; 


    private void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown("A")) Shoot();
        if (Input.GetButton("B")) Grapple();
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
        }
    }

    // private Quaternion CalculateRotation() 
    // {
    //     var rotationAngle = Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg;
    //     return Quaternion.Euler(0, 0, rotationAngle);
    // }
}
