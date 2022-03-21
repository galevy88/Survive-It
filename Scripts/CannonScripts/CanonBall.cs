using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBall : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] float damage = 100f;
    GameObject hitEffect;
    AudioClip ballBomb;


    // detact for hit of the cannon ball
    private void OnCollisionEnter(Collision collision)
    {

        // adjust effect for all gameObject expect terrain
        if (collision.gameObject.tag != "terrain")
        {
            // start hit effect
            GameObject impact = Instantiate(hitEffect, collision.gameObject.transform.position, Quaternion.identity);
            audioSource.PlayOneShot(ballBomb);
            // destroy hit effect
            Destroy(impact, 1f);
            // if canon ball hit enemy, kill enemy
            if (collision.gameObject.tag == "enemy")
            {
                EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
                enemyHealth.TakeDamage(damage);
            }
        }
        // destroy canon ball after hittting something
        Destroy(gameObject);

    }

    // hit effect of explosion when the cannon ball hit something
    public void SetEffect(GameObject obj)
    {
        hitEffect = obj;
    }
    // sound effect of explosion when the cannon ball hit something
    public void SetSound(AudioClip audio, AudioSource audioSourceCannon)
    {
        ballBomb = audio;
        audioSource = audioSourceCannon;
    }

}
