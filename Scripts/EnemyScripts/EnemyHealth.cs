using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    ZombieCounter zombieCounter;
    private float health = 100f;
    private bool isDead;

    private void Start()
    {
        isDead = false;
        zombieCounter = FindObjectOfType<ZombieCounter>();
    }

    // decrase enemy health by hit
    public void TakeDamage(float damage)
    {
        health -= damage;
        // kill zombie if hp < 0
        if(health <= 0)
        {
            Die();
        }
    }

    // when the enemy "killed" the Die animation is activate but actually the enemy is stiil on the ground
    // only after 20 sec his game object has been destroyed.
    // Here we return the status of the zombie which handling by the EnemyAi class
    public bool IsDead()
    {
        return isDead;
    }

    // if the zombie is Killed right now, do the appropriate stats
    private void Die()
    {
        // if the zombie is already, return and ignore
        if (isDead) return;

        // otherwise change the status of the zombie to die, show apropriate animations and 
        // disable the collider of the enemy so that the player will not collide him
        // also, decrase the zombie number in the scene by 1
        isDead = true;
        GetComponent<Animator>().SetBool("attack", false);
        GetComponent<Animator>().SetBool("die", true);
        GetComponent<CapsuleCollider>().enabled = false;
        zombieCounter.DecreaseNumberOfZombie();
    }
}
