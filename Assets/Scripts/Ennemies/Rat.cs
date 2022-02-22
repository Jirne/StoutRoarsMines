using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : Ennemy
{
    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {

        RaycastHit2D hitInfo = Physics2D.CircleCast(gameObject.transform.position, 5, Vector2.up, 10, playerMask);
        if (hitInfo.collider != null) {
            if (action == State.Patrol)
                moveSpeed++;
            action = State.Tracking;
        }
        else {
            if (action == State.Tracking)
                Reset(2);
        }

        switch (action) {
            case State.Patrol:
                if (gameObject.transform.position.x <= left) {
                    direction.x = 1;
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;

                }
                else if (gameObject.transform.position.x >= right) {
                    direction.x = -1;
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;
                }
                break;

            case State.Tracking:
                if(Player.Instance.transform.position.x - gameObject.transform.position.x > 0) {
                    direction.x = 1;
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                }
                else {
                    direction.x = -1;
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;
                }
                break;
        }


        Vector3 targetVelocity = new Vector2(moveSpeed * direction.x, m_Rigidbody2D.velocity.y);
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, 0.05f);

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.transform.CompareTag("Player")) {
            PlayerHealth p = collision.GetComponent<PlayerHealth>();
            p.TakeDamage(1);
        }
    }
}