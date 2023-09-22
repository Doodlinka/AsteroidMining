using UnityEngine;

public class Bullet : MonoBehaviour
{
    // [SerializeField] private float lifetime = 5f;
    private int damage = 1;
    private Rigidbody2D rb2d;
    
    private void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
        // Destroy(gameObject, lifetime);
    }

    public void Fire(Vector2 dir, float vel, int dmg) {
        if (rb2d == null) {
            rb2d = GetComponent<Rigidbody2D>();
        }
        rb2d.AddForce(dir * vel, ForceMode2D.Impulse);
        damage = dmg;
    }

    private void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.TryGetComponent<IDamageable>(out IDamageable d)) {
            d.TakeDamage(damage);
        }
        // TODO: explosion animation
        Destroy(gameObject);
    }
}
