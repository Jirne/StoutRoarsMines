using System;
using System.Linq;
using UnityEngine;

public class ItemWorld : MonoBehaviour {

    public Item item;
    public bool isRandom;
    

    private void Awake() {
        //Le type d'item sera g�n�r� al�atoirement, edn attendant voil�
        if (isRandom) {
            item.itemType = (Item.ItemType)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(Item.ItemType)).Length);
        }
        gameObject.GetComponent<SpriteRenderer>().sprite = item.GetSprite();
    }

    public void DestroySelf() {
        Destroy(gameObject);
    }

}
