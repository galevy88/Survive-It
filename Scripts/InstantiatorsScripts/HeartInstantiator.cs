using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartInstantiator : MonoBehaviour
{
    // heart prefab
    [SerializeField] GameObject heart;
    // time between instantiations
    [SerializeField] float induceTime = 15f;

    // at the beginning instantiate new prefab
    private void Start()
    {
        Instantiate(heart, transform.position, new Quaternion(0, 0, 0, 0));
    }

    // when the heart is taken, instantiate new bullet prefab after "induceTime" seconds
    public void GenereteNewHeart()
    {
        Invoke("InstantiateNewHeart", induceTime);
    }

    // instantiate new bullet
    private void InstantiateNewHeart()
    {
        Instantiate(heart, transform.position, new Quaternion(0, 0, 0, 0));
    } 
}
