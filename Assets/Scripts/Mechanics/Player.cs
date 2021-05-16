using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public CharacterController2D controller;
    public float runSpeed = 40f;

    private Inventory inventory;

    float horizontalMove = 0f;
    bool jump = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        Instance = this;
        inventory = new Inventory();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal")* runSpeed;
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        
    }

    private void FixedUpdate()
    {
        //Move the character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.transform.CompareTag("Coin")) {
            inventory.AddCoins(1);
            Coin c = collision.GetComponent<Coin>();
            c.DestroySelf();
        }
    }
}
