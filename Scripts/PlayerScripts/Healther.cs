using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healther : MonoBehaviour
{
    // amout of reward
    float enteryReward = 1f;
    PlayerHealth playerHealth;

    // caching
    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }
    // every time player goes inside the Healthers triger he got reward health
    private void OnTriggerEnter(Collider other)
    {
        playerHealth.AddHealth(enteryReward);
    }
}
