using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private UI_Inventory uiInventory;
    public Inventory Inventory { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        Instance = this;
        Inventory = new Inventory();
        uiInventory.SetInventory(Inventory);
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        ItemWorld itemWorld = collision.GetComponent<ItemWorld>();
        if(itemWorld != null) {
            Inventory.AddItem(itemWorld.item);
            itemWorld.DestroySelf();
        }
    }
}
