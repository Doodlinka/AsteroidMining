using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    Rigidbody2D rb; // im getting the rigidbody from the player just because i dont want any dependencies from the playermovement script
    [SerializeField]private float bulletImpulse = 1;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Fire1")) Shoot();
    }

    private void Shoot()
    {
        var rotation = CalculateRotation();
        GameObject b = Instantiate(bulletPrefab, shootPoint.position, rotation);
        b.GetComponent<FatBullet>().Fire(transform.up, bulletImpulse, 1);
        rb.AddForce(-transform.up * bulletImpulse, ForceMode2D.Impulse);
    }

    private Quaternion CalculateRotation() 
    {
        var rotationAngle = Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, rotationAngle);
    }
}
