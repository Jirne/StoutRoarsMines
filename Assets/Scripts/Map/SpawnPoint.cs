using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public int openningDir;
    //1 => Need left openning
    //2 => Need bottom openning
    //3 => Need right openning
    //4 => Need up openning

    private bool spawned = false;

    private const int ROOM_TRESHOLD = 10;

    private List<int> interdictions = new List<int>();
    private List<GameObject> pool = new List<GameObject>();

    private List<String> PathEnds = new List<String> { "R", "L", "B", "T" };
    private List<String> BigPaths = new List<string> { "LRT", "LRB" };

    private void Start() {
        Invoke("Spawn",.1f);
    }

    private void Spawn() {
        if (spawned == false) {

            this.pool = ParsePool(openningDir);

            for (int i = 0; i < pool.Count; i++) {
                if (PathEnds.IndexOf(pool[i].name) != -1 && RoomTemplate.Instance.RoomGenerated < ROOM_TRESHOLD && pool.Count > 1) {
                    //Debug.Log("Solo : " + pool[i].name+"/"+ RoomTemplate.Instance.RoomGenerated);
                    pool.RemoveAt(i);
                    break;
                }
            }

            SpawnRoom();
        }
        
    }

    private void SpawnFused(int oD1, int oD2) {
        //Prendre l'intersection des deux Listes
        List<GameObject> pooloD1 = ParsePool(oD1);
        List<GameObject> pooloD2 = ParsePool(oD2);

        for (int i = 0; i < pooloD1.Count; i++) {
            for (int j = 0; j < pooloD2.Count; j++) {
                if (pooloD1[i].name.Equals(pooloD2[j].name)) {
                    pool.Add(pooloD1[i]);
                }
            }
        }


        for (int i = 0; i < pool.Count; i++) {
            if (PathEnds.IndexOf(pool[i].name) != -1 && RoomTemplate.Instance.RoomGenerated < ROOM_TRESHOLD && pool.Count > 1) {
                Debug.Log("Fused : " + pool[i].name + "/" + RoomTemplate.Instance.RoomGenerated);
                pool.RemoveAt(i);
                break;
            }

        }

        bool hasbigpath = false;
        for (int i = 0; i < pool.Count; i++) {
            if (RoomTemplate.Instance.RoomGenerated < ROOM_TRESHOLD && BigPaths.IndexOf(pool[i].name) != -1) {
                hasbigpath = true;
                break;
            }
        }

        if (hasbigpath) {
            for (int i = 0; i < pool.Count; i++) {
                if (pool[i].name.Length < 3) {
                    pool.RemoveAt(i);
                }
            }
        }



        for (int i = 0; i < pool.Count; i++) {
            //Debug.Log(pool[i].name);
        }

        SpawnRoom();
    }

    private void SpawnRoom(){
        var newRoom = Instantiate(pool[UnityEngine.Random.Range(0, pool.Count)], transform.position, Quaternion.identity);
        newRoom.transform.parent = GameObject.Find("FullMap").transform;
        spawned = true;
        RoomTemplate.Instance.RoomGenerated++;
    }


    private List<GameObject> ParsePool(int direction) {
        List<GameObject> tmpPool = new List<GameObject>();
        switch (direction) {
            case 1:
                for (int i = 0; i < RoomTemplate.Instance.LeftOpenning.Length; i++) {
                    GameObject tmpObj = RoomTemplate.Instance.LeftOpenning[i];
                    if (CanIAddToThePool(tmpObj.transform, "RoomSpawn", interdictions)) {
                        tmpPool.Add(tmpObj);
                    }
                }
                break;
            case 2:
                for (int i = 0; i < RoomTemplate.Instance.DownOpenning.Length; i++) {
                    GameObject tmpObj = RoomTemplate.Instance.DownOpenning[i];
                    if (CanIAddToThePool(tmpObj.transform, "RoomSpawn", interdictions)) {
                        tmpPool.Add(tmpObj);
                    }
                }
                break;
            case 3:
                for (int i = 0; i < RoomTemplate.Instance.RightOpenning.Length; i++) {
                    GameObject tmpObj = RoomTemplate.Instance.RightOpenning[i];
                    if (CanIAddToThePool(tmpObj.transform, "RoomSpawn", interdictions)) {
                        tmpPool.Add(tmpObj);
                    }
                }
                break;
            case 4:
                for (int i = 0; i < RoomTemplate.Instance.UpOpenning.Length; i++) {
                    GameObject tmpObj = RoomTemplate.Instance.UpOpenning[i];
                    if (CanIAddToThePool(tmpObj.transform, "RoomSpawn", interdictions)) {
                        tmpPool.Add(tmpObj);
                    }
                }
                break;
        }
        return tmpPool;

    }


    public bool CanIAddToThePool(Transform parent, string _tag, List<int> forbidDirs) {
        bool allowToPool = true;
        for (int i = 0; i < parent.childCount; i++) {
            Transform child = parent.GetChild(i);
            if (child.tag == _tag && forbidDirs.IndexOf(child.GetComponent<SpawnPoint>().openningDir) != -1) {
                allowToPool = false;
            }
        }
        return allowToPool;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("FakeSpawn")) {
            spawned = true;
        }
        if (collision.CompareTag("RoomSpawn")) {
            if (spawned == false && collision.GetComponent<SpawnPoint>().spawned == false) {
                //Debug.Log(this.transform.position.x+"/"+this.transform.position.y);
                if(this.GetInstanceID() < collision.GetInstanceID()) {

                    /*
                    List<int> openningDirs = new List<int> {
                        this.openningDir,
                        collision.GetComponent<SpawnPoint>().openningDir
                    };
                    */
                    collision.GetComponent<SpawnPoint>().SpawnFused(this.openningDir, collision.GetComponent<SpawnPoint>().openningDir);
                }
            }

            spawned = true;
        }

        if (collision.CompareTag("ForbidSpawn")) {
            interdictions.Add(collision.GetComponent<ForbidPoint>().forbidDirection);
        }
    }
}
