using UnityEngine;

public class LimitedLifetime : MonoBehaviour
{
    [SerializeField] private float lifetime;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

}
