using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.SceneManagement;
// using System.Collections;

// TODO: put the score elsewhere or rename the script
public class PlayerStats : MonoBehaviour, IDamageable
{
    [SerializeField] private Text scoreText, hiText;
    [SerializeField] private Image livesBar;
    [SerializeField] private int maxLives = 3;
    [SerializeField] private float iframes;
    [SerializeField] private AudioClip[] hurtSounds;//, boomSounds;
    private AudioSource audioSource;
    private float invTimer;
    private int score, hi, lives;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        lives = maxLives;
        if (PlayerPrefs.HasKey("HI")) {
            hi = PlayerPrefs.GetInt("HI");
        }
        hiText.text = "HI " + hi.ToString().PadLeft(3, '0');
        Respawn();
    }

    void FixedUpdate() {
        if (invTimer > 0) {
            invTimer -= Time.fixedDeltaTime; 
        } 
    }

    private void Respawn() {
        // TODO: update lives ui
        // transform.position = Vector3.zero;
        invTimer = iframes;
        livesBar.fillAmount = (float)lives / maxLives;
    }

    public void TakeDamage(int damage = 1) {
        if (invTimer > 0) return;

        lives -= 1;
        if (lives <= 0) {
            Die();
        }
        Respawn();
        audioSource.PlayOneShot(hurtSounds[Random.Range(0, hurtSounds.Length)]);
    }

    public void Die() {
        if (score > hi) {
            PlayerPrefs.SetInt("HI", score);
            hiText.text = "HI " + score.ToString().PadLeft(3, '0');
        }
        // TODO: explosion animation
        Destroy(gameObject);
        // AudioSource.PlayClipAtPoint(boomSounds[Random.Range(0, boomSounds.Length)], transform.position, 1.2f);
        // StartCoroutine("ReloadScene");
    }

    // private IEnumerator ReloadScene() {
    //     yield return new WaitForSeconds(3);
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //     yield break;
    // }

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
