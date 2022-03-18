using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAudio : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip walk;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // if the player walk, play sound of walking
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            // if there is no audio playing, play the sound
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(walk);
            }

        }
        // if the audio is already play dont play the sound
        else
        {
            audioSource.Stop();
        }
    }
}
