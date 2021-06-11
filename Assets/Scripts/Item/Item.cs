using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType {
        Coin,
        HealthPotion
    }

    public ItemType itemType;


    public Sprite GetSprite() {
        switch (itemType) {
            default:
            case ItemType.Coin:         return ItemAssets.Instance.coinSprite;
            case ItemType.HealthPotion: return ItemAssets.Instance.healthPotionSprite;
        }
    }
}
