using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // ammo scripts of the player
    Ammo ammo;
    BulletInstantiator bulletInstantiator;
    [SerializeField] int ammoValue = 10;

    // chaching for required components
    private void Start()
    {
        ammo = FindObjectOfType<Ammo>();
        bulletInstantiator = FindObjectOfType<BulletInstantiator>();
    }
    // when collison ocures if the collison is the player, reward him
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "player")
        {
            // reward player by ammo
            ammo.AddAmmo(ammoValue);
            // generete new bullet prefab because this one just taken
            bulletInstantiator.GenereteNewBullet();
            // destroy the current game object.
            Destroy(gameObject);
        }

    }
}
