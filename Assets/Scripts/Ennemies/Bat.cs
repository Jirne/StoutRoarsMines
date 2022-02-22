using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Ennemy
{

void FixedUpdate() {
    RaycastHit2D hitInfo = Physics2D.CircleCast(gameObject.transform.position, 5, Vector2.up, 10, playerMask);
        if (hitInfo.collider != null) {
            if (action == State.Patrol)
                moveSpeed++;
            action = State.Tracking;
        }
        else {
            if (action == State.Tracking)
                Reset(4);
        }

        switch (action) {
            case State.Patrol:
                if (gameObject.transform.position.x <= left) {
                    direction.x = 1;
                }
                else if (gameObject.transform.position.x >= right) {
                    direction.x = -1;
                }
                direction.y = 0;
                break;

            case State.Tracking:
                if(Player.Instance.transform.position.x - gameObject.transform.position.x > 0) 
                    direction.x = 1;
                else 
                    direction.x = -1;
                

                if(Player.Instance.transform.position.y+2 - gameObject.transform.position.y > 0) 
                    direction.y = 1;
                else
                    direction.y = -1;

                break;
        }

        gameObject.transform.localScale = new Vector3 (-direction.x,gameObject.transform.localScale.y,0);
        Vector3 targetVelocity = new Vector2(moveSpeed * direction.x, 3*direction.y);
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, 0.05f);
    }
}
