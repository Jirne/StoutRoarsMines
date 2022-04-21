using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject map;

    public string[] bossScenes;
    public Door door;
    void Start()
    {
        map = GameObject.Find("FullMap");
        Invoke("SpawnBoss",5f);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //TODO detection de fin de génération de niveau pour call SpawnBoss
        
    }

    private void SpawnBoss(){
        Transform room = map.transform.GetChild(map.transform.childCount - 1);

        //TODO : Positionner proprement la porte au lieu d'au milieu :  DoorSpawner ?
        var bossDoor = Instantiate(door, room.position, Quaternion.identity);
        bossDoor.transform.parent = room;
        bossDoor.bossScene = "Rat";
    }
}
