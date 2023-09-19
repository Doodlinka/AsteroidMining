using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    private Rigidbody2D _rb;
    
    private void Start() =>
        _rb = GetComponent<Rigidbody2D>();

    private void FixedUpdate() =>
        _rb.velocity = transform.right * speed;

    private void OnCollisionEnter2D(Collision2D other) =>
            Destroy(gameObject);
}
