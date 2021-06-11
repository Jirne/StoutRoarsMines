using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplate : MonoBehaviour {
    public static RoomTemplate Instance { get; private set; }

    public GameObject[] UpOpenning;
    public GameObject[] DownOpenning;
    public GameObject[] RightOpenning;
    public GameObject[] LeftOpenning;

    private int roomGenerated;
    public int RoomGenerated { get; set; }

    private void Awake() {
        Instance = this;
        RoomGenerated = 0;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
