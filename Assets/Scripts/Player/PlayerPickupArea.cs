using UnityEngine;

// TODO: put the score elsewhere or rename the script
public class PlayerPickupArea : MonoBehaviour
{
    private int score;

    void OnTriggerEnter2D (Collider2D other) {
        if (other.TryGetComponent<Asteroid>(out Asteroid rock)) {
            if (rock.Size == 1) {
                if (rock.HasOre) {
                    score += 5;
                }
                else {
                    score += 1;
                }
                rock.Vanish();
                Debug.Log(score);
            }
        }
    } 
}
