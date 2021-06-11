using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {

    public Animator animator;
    //public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;


    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            Attack();
        }
    }

    void Attack() {
        //Play Attack Animation
        animator.SetTrigger("Attack");

        //Detect ennemies
        /*
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        //Apply Damage
        foreach(Collider2D enemy in hitEnemies) {
            Debug.Log("Hit !");
            enemy.GetComponent<Ennemy>().TakeDamage(1);
        }
        */
    }
}
