using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{

    private Inventory inventory;
    private Transform CoinCount;
    private Transform PotionCount;

    public void SetInventory(Inventory inventory) {
        this.inventory = inventory;

        inventory.OnInventoryChanged += Inventory_OnInventoryChanged;
        RefreshInventory();
    }

    private void Inventory_OnInventoryChanged(object sender, System.EventArgs e) {
        RefreshInventory();
    }

    private void Awake() {
        PotionCount = transform.Find("HealthPotionTracker/PotionCount");
        CoinCount = transform.Find("CoinTracker/CoinCount");
    }

    private void RefreshInventory() {
        TextMeshProUGUI coinUiText = CoinCount.GetComponent<TextMeshProUGUI>();
        coinUiText.SetText("x " + inventory.Coin.ToString());

        TextMeshProUGUI potionUiText = PotionCount.GetComponent<TextMeshProUGUI>();
        potionUiText.SetText("x " + inventory.Potion.ToString());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
