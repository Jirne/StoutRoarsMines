using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory {
    public int Coin { get; set; }
    public int Potion { get; set; }
    private const int MAX_POTION = 20;

    public event EventHandler OnInventoryChanged;

    public GameObject coinText = GameObject.Find("CoinCount");


    public Inventory() {
        Coin = 0;
        Potion = 0;
    }
    public void AddItem(Item i) {
        switch (i.itemType) {
            case Item.ItemType.Coin: 
                this.Coin++;
                break;
            case Item.ItemType.HealthPotion:
                if (Potion < MAX_POTION)
                    this.Potion++;
                break;
            default:
                //Ca partira dans l'inventaire normal
                break;

        }

        OnInventoryChanged?.Invoke(this, EventArgs.Empty);
    }

    public void DeleteItem(Item i) {
        switch (i.itemType) {
            case Item.ItemType.Coin:
                if(this.Coin > 0) {
                    this.Coin--;
                }
                break;
            case Item.ItemType.HealthPotion:
                if (Potion > 0)
                    this.Potion--;
                break;
            default:
                //Ca partira dans l'inventaire normal
                break;

        }

        OnInventoryChanged?.Invoke(this, EventArgs.Empty);
    }
}
