using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    private const int BASE_HEALTH = 10;
    public int health;
    public Component HealthBar;
    public YingYang uiData;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Heal")) {
            Heal(1);
        }
        HealthBar.GetComponent<Slider>().SetValueWithoutNotify((float)health / (float)BASE_HEALTH);
        uiData.playerHealth = health;
        uiData.playerMaxHealth = BASE_HEALTH;
    }

    public void TakeDamage (int dmg) {
        health -= dmg;
        Update();
    }

    public void Heal(int dmg) {
        if(health < BASE_HEALTH && Player.Instance.Inventory.Potion > 0) {
            Debug.Log("Heal !");
            health += dmg;
            Player.Instance.Inventory.DeleteItem(new Item { itemType = Item.ItemType.HealthPotion });
            Update();
        }
    }
}
