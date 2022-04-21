using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public string bossScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    private void OnTriggerStay2D(Collider2D other) {
        if (other.transform.CompareTag("Player") && Input.GetAxisRaw("Vertical") != 0) {
            Debug.Log("Open Boss Scene" +bossScene);
        }
    }
}
