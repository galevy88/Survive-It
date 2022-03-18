using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Methor : MonoBehaviour
{
    PlayerHealth playerHealth;
    [SerializeField] float damage = 20f;

    // caching for player health component
    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }


    private void Update()
    {
        // in extreme situation which the methor goes out of bound
        if (gameObject.transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    // if the methor hit something do apropriate action
    private void OnCollisionEnter(Collision collision)
    {
        // if the methor hit the terrain destroy him
        if(collision.gameObject.tag == "terrain")
        {
            Destroy(gameObject);
        }

        // if the methor hit the player take damage
        if(collision.gameObject.tag == "player")
        {
            playerHealth.TakeDamage(damage);
        }
    }

}
