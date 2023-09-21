using UnityEngine;

public class Asteroid : MonoBehaviour, IDamageable
{
    [SerializeField] private Sprite[] sprites, oreSprites;

    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    private CircleCollider2D coll;

    private int size = 1, health;
    private int Size {
        get => size;
        set {
            size = value;
            // transform.localScale = new Vector3(size, size, 1);
            if (rb2d != null) {
                rb2d.mass = size;
            }
            if (coll != null) {
                coll.radius = size / 2f;
            }
            health = size;
        }
    }
    private bool hasOre;
    public bool HasOre => hasOre;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<CircleCollider2D>();
        Init(Random.Range(1, 4), Random.insideUnitCircle.normalized * Random.Range(2f, 8f), Random.value >= 0.75);
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
            Instantiate(gameObject).GetComponent<Asteroid>().Init(Size - 1, rb2d.velocity, hasOre);
            hasOre = Random.value >= 0.75;
            SetSprite();
        }
        else {
            Vanish();
        }
    }

    // used to silently despawn
    // this won't give any animation or score, Die handles that
    // needs to be handled in the pooler, maybe entirely
    public void Vanish() {
        Destroy(gameObject);
    }

    public void Init(int _size, Vector2 velocity, bool ore) {
        Size = _size;
        rb2d.velocity = velocity;
        hasOre = ore;
        SetSprite();
    }

    private void SetSprite() {
        if (sr == null) return;
        if (hasOre) {
            sr.sprite = oreSprites[size - 1];
        }
        else {
            sr.sprite = sprites[size - 1];
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.TryGetComponent<IDamageable>(out IDamageable d)) {
            d.TakeDamage(size);
        }
    }
}
