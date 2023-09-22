using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    Rigidbody2D rb; 
    [SerializeField]private float bulletImpulse = 1;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetButtonUp("A")) Shoot();
    }

    private void Shoot()
    {
        var rotation = CalculateRotation();
        GameObject b = Instantiate(bulletPrefab, shootPoint.position, rotation);
        b.GetComponent<Bullet>().Fire(transform.up, bulletImpulse, 1);
        rb.AddForce(-transform.up * bulletImpulse, ForceMode2D.Impulse);
    }

    private Quaternion CalculateRotation() 
    {
        var rotationAngle = Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, rotationAngle);
    }
}
