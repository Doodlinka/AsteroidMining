using UnityEngine;

public class Asteroid : MonoBehaviour, IDamageable
{
    private Rigidbody2D rb2d;

    private int size = 1, health;
    private int Size {
        get => size;
        set {
            size = value;
            transform.localScale = new Vector3(size, size, 1);
            if (rb2d != null) {
                rb2d.mass = size;
            }
            health = size;
        }
    }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Init(Random.Range(1, 4), Random.insideUnitCircle.normalized * Random.Range(2f, 8f));
    }

    public void TakeDamage(int damage = 1) {
        health -= damage;
        if (health <= 0) {
            Die();
        }
    }

    public void Die() {
        // TODO: explosion animation
        if (Size > 1) {
            Size--;
            // TODO: change velocity on split and maybe shift the init burden on the spawner
            Instantiate(gameObject).GetComponent<Asteroid>().Init(Size - 1, rb2d.velocity);
        }
        else {
            Destroy(gameObject);
        }
    }

    public void Init(int _size, Vector2 velocity) {
        Size = _size;
        rb2d.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.TryGetComponent<IDamageable>(out IDamageable d)) {
            d.TakeDamage(size);
        }
    }
}
