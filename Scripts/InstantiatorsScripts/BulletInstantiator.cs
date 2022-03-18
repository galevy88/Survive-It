using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInstantiator : MonoBehaviour
{
    // bullet prefab
    [SerializeField] GameObject bullet;
    // time between instantiations
    [SerializeField] float induceTime = 10f;

    // at the beginning instantiate new prefab
    private void Start()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);

    }
    // when the bullet is taken, instantiate new bullet prefab after "induceTime" seconds
    public void GenereteNewBullet()
    {
        Invoke("InstantiateNewBullet", induceTime);
    }

    // instantiate new bullet
    private void InstantiateNewBullet()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }



}
