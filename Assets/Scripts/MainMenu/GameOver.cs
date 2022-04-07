using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    const int MAX_TEMPO = 1000;
    private int i = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        i++;
        if (i >= MAX_TEMPO)
            SceneManager.LoadScene("MainMenu");
        
    }
}
