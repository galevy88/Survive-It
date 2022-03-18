using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstantiator : MonoBehaviour
{
    // zombie counter of currently how much zombie alive in the scene
    ZombieCounter zombieCounter;
    // enemyAi component to set target as player
    EnemyAi enemy;
    // enemy prefab
    [SerializeField] GameObject zombie;
    // time gap between instantiations, actually determined in the inspector
    [SerializeField] float timeGap = 2f;
    // zombie number in each scene, actuualy determined in the insperctor
    [SerializeField] int zombieNumber = 10;
    // player transform to set to enemyAi
    [SerializeField] Transform player;
    bool canInstantiate = true;

    // caching to component
    private void Start()
    {
        enemy = zombie.GetComponent<EnemyAi>();
        enemy.SetTargetOneTime(player);
        zombieCounter = FindObjectOfType<ZombieCounter>();

    }

    private void Update()
    {
        // if the zombie number > 0 thus there is another zombie we need to instaniate and also we can
        // instantiate by the canInstantiate bool, we will create new Zombie
        if(canInstantiate && zombieNumber > 0)
        {
            // start corutine whice instantiate new zombie every timeGap seconds
            StartCoroutine(CreateNewZombie());
            // increase the zombie number that we will have to instantiate by 1
            zombieNumber--;
        }

    }

    // creating new zombie
    IEnumerator CreateNewZombie()
    {
        // because we just create a zombie right now we will set can instantiate to false after the corutine will end
        canInstantiate = false;
        // determine z position randomly
        float zPos = GenereteZAxisPosition();
        // create a vector of creation the enemy
        Vector3 instantiatePos = new Vector3(transform.position.x, transform.position.y, zPos);
        Instantiate(zombie, instantiatePos, Quaternion.identity);
        // update the number of the zombie that alive right now by 1
        zombieCounter.IncreaseNumberOfZombie();
        // cortuine managment
        yield return new WaitForSeconds(timeGap);
        canInstantiate = true;
    }

    // generete z position randomly
    private float GenereteZAxisPosition()
    {
        float random = Random.Range(5f, 40f);
        return random;
    }
}
