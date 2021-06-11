using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{

    private static ItemAssets instance;
    public static ItemAssets Instance {
        get {
            if (instance == null)
                instance = FindObjectOfType(typeof(ItemAssets)) as ItemAssets;

            return instance;
        }
        set {
            instance = value;
        }
    }

    private void Awake() {
        Instance = this;
    }

    public Sprite coinSprite;
    public Sprite healthPotionSprite;
}
