using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour, IDamageable
{
    [SerializeField] private Sprite[] sprites, oreSprites;
    // [SerializeField] private AudioClip[] boomSounds;

    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    private CircleCollider2D coll;

    // DONT'T GET THE BACKER
    private AsteroidData _data;
    public AsteroidData Data {
        get {
            _data.Position = transform.position;
            _data.Velocity = rb2d?.velocity ?? Vector2.zero;
            return _data;
        }
    }
    public int Size {
        get => _data.Size;
        private set {
            _data.Size = value;
            if (rb2d == null) rb2d = GetComponent<Rigidbody2D>();
            if (coll == null) coll = GetComponent<CircleCollider2D>();
            rb2d.mass = _data.Size;
            coll.radius = _data.Size / 2f;
            health = _data.Size;
            SetSprite();
        }
    }
    public bool HasOre  {
        get => _data.HasOre;
        private set {
            _data.HasOre = value;
            SetSprite();
        }
    }
    private int health;

    public event Action<AsteroidData> onSplit;
    // [HideInInspector]public bool SubscribedToEvent = false;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<CircleCollider2D>();
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
            onSplit?.Invoke(Data);
        }
        else {
            Vanish();
        }
        // AudioSource.PlayClipAtPoint(boomSounds[Random.Range(0, boomSounds.Length)], transform.position, 1.2f);
    }

    // used to silently despawn
    // this won't give any animation or score, Die handles that
    public void Vanish() {
        // return to the pool if we keep it
        Destroy(gameObject);
    }

    public void Init(AsteroidData data) {
        Size = data.Size;
        transform.position = data.Position;
        rb2d.velocity = data.Velocity;
        HasOre = data.HasOre;
        // bad hardcode but i don't care
        rb2d.rotation = Random.Range(0f, 360f);
        rb2d.angularVelocity = Random.Range(-10f, 10f);
        // rb2d.AddForce(Random.insideUnitSphere * 3, ForceMode2D.Impulse);
    }

    private void SetSprite() {
        if (sr == null) sr = GetComponent<SpriteRenderer>();
        if (HasOre) {
            sr.sprite = oreSprites[Size - 1];
        }
        else {
            sr.sprite = sprites[Size - 1];
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.TryGetComponent<IDamageable>(out IDamageable d)) {
            d.TakeDamage(Size);
        }
    }

    // die if out of bounds
    private void OnTriggerExit2D(Collider2D other) {
        // TODO: name check bad, replace with something else (what?)
        if (other.gameObject.name == "RockArea") {
            Vanish();
        }
    }

    // private void OnDisable() {
    //     // return to the pool if we keep it
    //     // Vanish()?
    // }

    // private void OnDestroy() {
    //     // return to the pool if we keep it
    //     // Vanish()?
    // }
}
