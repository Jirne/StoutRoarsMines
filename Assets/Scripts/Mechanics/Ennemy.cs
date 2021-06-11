using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D m_Rigidbody2D;
    public int moveSpeed;
    private int direction = -1;
    private Vector3 m_Velocity = Vector3.zero;
    private float left;
    private float right;
    public LayerMask playerMask;
    private State action;

    private enum State {
        Tracking,
        Patrol
    }

    void Start()
    {
        
    }

    void Awake() {
        Reset();
        m_Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int dmg) {
        Destroy(gameObject);
    }

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
                Reset();
        }

        switch (action) {
            case State.Patrol:
                if (gameObject.transform.position.x <= left) {
                    direction = 1;
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;

                }
                else if (gameObject.transform.position.x >= right) {
                    direction = -1;
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;
                }
                break;

            case State.Tracking:
                if(Player.Instance.transform.position.x - gameObject.transform.position.x > 0) {
                    direction = 1;
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                }
                else {
                    direction = -1;
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;
                }
                break;
        }


        Vector3 targetVelocity = new Vector2(moveSpeed * direction, m_Rigidbody2D.velocity.y);
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, 0.05f);
        //Debug.Log(action.ToString());

    }

    private void Reset() {
        action = State.Patrol;
        moveSpeed--;
        left = gameObject.transform.position.x - 2f;
        right = gameObject.transform.position.x + 2f;

    }

    void OnDrawGizmos() {
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(transform.position, 5);
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.transform.CompareTag("Player")) {
            PlayerHealth p = collision.GetComponent<PlayerHealth>();
            p.TakeDamage(1);
        }
    }
}