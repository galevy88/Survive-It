using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    // the target of the enemyAi is the player and it's assign in the inspector
    [SerializeField] Transform target;
    // every fragme distanceToTarget is calculted and it is neccessery to NavMeshAi
    float distanceToTarget;
    // NavMeshAi Component
    NavMeshAgent navMeshAgent;
    EnemyAttack enemyAttack;
    EnemyHealth enemy;
    // time that required to destroy gameObject after dying in order to don't make an overload on the game
    float timeDestroyWait = 20f;
    // turn speed for FaceRotaion
    [SerializeField] float turnSpeed = 5f;

    // chaching for all the Components in the start method
    void Start()
    {
        enemyAttack = GetComponent<EnemyAttack>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<EnemyHealth>();
    }

    // because enemy is instantiate as a prefab by enemy instantiators its required to
    // set player as a target by enemy instantiators every scene. the enemy instantiators
    // have a serialzieFiled of the player so that he assign to each zombie he create a target.
    public void SetTargetOneTime(Transform player)
    {
        target = player;
    }

    void Update()
    {
        // if the zombie is dead (lying on the ground) disabled him and his NavMashAi component
        // so that he will not move and chasing after the player while he is dead.
        // also, destroy his gameObject after certain amount of time
        if (enemy.IsDead())
        {
            enabled = false;
            navMeshAgent.enabled = false;
            Destroy(gameObject, timeDestroyWait);
        }
        // if the zombie is alive, shows is walking animation and activate NavMashAi options
        else
        {
            GetComponent<Animator>().SetTrigger("move");
            SetTarget();
            CheckForAttacking();
        }


    }
    // NavMeshAi method which set player as destination of the Ai
    // calculating the distance between the Ai to the player for each frame.
    // also handling rotaions of the enemy to face to the player
    private void SetTarget()
    {
        navMeshAgent.SetDestination(target.position);
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        FaceTarget();
    }

    // checking for attack if the distance is sufficent.
    // the sufficent distance is ruled by the stoppingDistance of the naveMeshAi in the inspector
    private void CheckForAttacking()
    {
        // if the disstance sufficent = attack
        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
        // otherwise dont show the attack animation
        else
        {
            GetComponent<Animator>().SetBool("attack", false);
        }
    }

    // if the distance is sufficent to attack, attack the player and show the animation.
    // the attack is generated by another class EnemyAttack which we called her method
    private void AttackTarget()
    {
        GetComponent<Animator>().SetBool("attack", true);
        enemyAttack.AttackHitEvent();
    }

    // Rotate the enemy to the player every single frame
    private void FaceTarget()
    {
        // set a vector of the enemy and the player and normalize it because we want only the direction
        Vector3 direction = (target.position - transform.position).normalized;

        // set Quaternion of the rotaion which it detrminate by the direction vector
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));

        // change the zombie rotaion by the Quaternion we just made and do it by slerping the movement by
        // the turn speed which control the speed that the enemy will make the rotation.
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }
}
