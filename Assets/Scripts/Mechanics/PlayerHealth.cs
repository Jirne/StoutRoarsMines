using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    private const int MAX_HEALTH = 20;
    public int currentMaxHealth;
    public int health;
    public HeadHealth uiData;

    void Awake(){
        UpdateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Heal")) {
            Heal(1);
        }
    }

    public void TakeDamage (int dmg) {
        health -= dmg;
        if(health <= 0){
            SceneManager.LoadScene("GameOver");
        }
        else{
            UpdateHealth();
            //Trigger invul state
            
        }
    }

    public void Heal(int dmg) {
        if(health < currentMaxHealth && Player.Instance.Inventory.Potion > 0) {
            Debug.Log("Heal !");
            health += dmg;
            Player.Instance.Inventory.DeleteItem(new Item { itemType = Item.ItemType.HealthPotion });
            UpdateHealth();
        }
    }

    void UpdateHealth(){
        uiData.UpdateHealth(health, currentMaxHealth, MAX_HEALTH);
    }
}
