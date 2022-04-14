using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.CompareTag("Ennemies")) {
            Ennemy e = collision.GetComponent<Ennemy>();
            e.TakeDamage(1);
        }
    }
}

