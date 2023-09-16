using UnityEngine;

public class Asteroid : MonoBehaviour
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
        // TODO: unhardcode sizes and speed
        Size = Random.Range(1, 4);
        rb2d.velocity = Random.insideUnitCircle.normalized * Random.Range(2f, 8f);
    }

    public void TakeDamage() {
        health--;
        if (health <= 0) {
            Die();
        }
    }

    private void Die() {
        if (Size > 1) {
            Size--;
            Instantiate(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
}
