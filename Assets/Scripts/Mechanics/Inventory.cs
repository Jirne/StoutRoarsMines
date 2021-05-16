using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour {
    private int coin = 0;

    public GameObject coinText = GameObject.Find("CoinCount");

    public Inventory() {
    }

    private void Awake() {
        
    }

    public void AddCoins(int amount) {
        this.coin += amount;
        TextMeshProUGUI uiText = coinText.GetComponent<TextMeshProUGUI>();
        uiText.SetText("x " + coin.ToString());
    }

    public void RemoveCoins(int amount) {
        this.coin -= amount;
        TextMeshProUGUI uiText = coinText.GetComponent<TextMeshProUGUI>();
        uiText.SetText("x " + coin.ToString());
    }
}
