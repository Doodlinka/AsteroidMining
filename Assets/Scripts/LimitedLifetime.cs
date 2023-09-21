using UnityEngine;

public class LimitedLifetime : MonoBehaviour
{
    [SerializeField] private float lifetime = 1;

    void Start()
    {
        Destroy(gameObject, lifetime);
        GetComponent<Animation>().Play();
    }

}
