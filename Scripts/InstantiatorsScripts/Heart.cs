using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    // playerHealth scripts of the player
    PlayerHealth playerHealth;
    HeartInstantiator heartInstantiator;
    [SerializeField] float healthValue = 10f;

    // chaching for required components
    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        heartInstantiator = FindObjectOfType<HeartInstantiator>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "player")
        {
            // reward player by health
            playerHealth.AddHealth(healthValue);
            // generete new heart prefab because this one just taken
            heartInstantiator.GenereteNewHeart();
            // destroy the current game object.
            Destroy(gameObject);
        }

    }
}
