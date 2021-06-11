using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        int r = UnityEngine.Random.Range(1, 4);
        if(r < 3) {
            Destroy(gameObject);
        }
    }
}
