using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    void Start()
    {
        // "Destroy me after 'lifetime' seconds"
        Destroy(gameObject, 2.5f);
    }
}