using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    // Start is called before the first frame update
    protected Rigidbody2D m_Rigidbody2D;
    public int moveSpeed;

    protected Vector2Int direction = new Vector2Int(-1,1);
    protected Vector3 m_Velocity = Vector3.zero;
    protected float left;
    protected float right;
    public LayerMask playerMask;
    protected State action;

    protected enum State {
        Tracking,
        Patrol
    }

    void Awake() {
        m_Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int dmg) {
        Destroy(gameObject);
    }

    protected void Reset(float patrolSize) {
        action = State.Patrol;
        moveSpeed--;
        left = gameObject.transform.position.x - patrolSize;
        right = gameObject.transform.position.x + patrolSize;

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