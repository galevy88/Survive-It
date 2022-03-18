using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // set a target to attack
    PlayerHealth target;
    [SerializeField] float damage = 40f;
    // the time gap between the situations that the enemy can attack
    [SerializeField] float attackInducerTime = 2f;
    bool canAttack;

    // set enemy to attack
    private void Start()
    {
        // set can attack as true because the enemy just created
        canAttack = true;
        // chaching for player health in order to take health when the enemy attack
        target = FindObjectOfType<PlayerHealth>();
    }
    public void AttackHitEvent()
    {
        if (target == null) return;
        if(canAttack)
        {
            // decrase player ealth
            canAttack = false;
            target.TakeDamage(damage);
            StartCoroutine(attackInducer());
        }
        
    }

    // inducer for enemy attack, zombie can attack ever 2 sec
   private IEnumerator attackInducer()
    {
        yield return new WaitForSeconds(attackInducerTime);
        canAttack = true;
    }
}
