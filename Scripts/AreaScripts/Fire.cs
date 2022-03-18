using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // the damage that the fire cause
    [SerializeField] float damage = 20f;

    // every time player goes inside the fire's triger he got hit
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "player")
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
