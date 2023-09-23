using UnityEngine;
using UnityEngine.UI;

// TODO: put the score elsewhere or rename the script
public class PlayerStats : MonoBehaviour, IDamageable
{
    [SerializeField] private Text scoreText, hiText;
    [SerializeField] private int maxHealth = 6;
    private int score, hi, health;

    void Start() {
        health = maxHealth;
        if (PlayerPrefs.HasKey("HI")) {
            hi = PlayerPrefs.GetInt("HI");
        }
        hiText.text = "HI " + hi.ToString().PadLeft(3, '0');
    }

    public void TakeDamage(int damage = 1) {
        health -= damage;
        if (health <= 0) {
            Die();
        }
    }

    public void Die() {
        // TODO: explosion animation
        if (score > hi) {
            PlayerPrefs.SetInt("HI", score);
            hiText.text = "HI " + score.ToString().PadLeft(3, '0');
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.TryGetComponent<IDamageable>(out IDamageable d)) {
            d.TakeDamage(2);
        }
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (other.TryGetComponent<Asteroid>(out Asteroid rock)) {
            if (rock.Size == 1) {
                if (rock.HasOre) {
                    score += 10;
                }
                else {
                    score += 1;
                }
                scoreText.text = "SCR " + score.ToString().PadLeft(3, '0');
                rock.Vanish();
            }
        }
    } 
}
