using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ZombieCounter : MonoBehaviour
{
    // scene Loader on the scene
    SceneLoader sceneLoader;
    // zombie alive in the level counter
    int zombieAlive;
    // GUI for zombie alive
    [SerializeField] TextMeshProUGUI zombieAliveTxt;

    // initial the number of the zombie in the scene and make caching for SceneLoader
    private void Start()
    {
        zombieAlive = 0;
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    // sjpt how many zombie in the scene every single frame
    void Update()
    {
        DisplayText();
    }

    // display zombieAlive on GUI
    private void DisplayText()
    {
        zombieAliveTxt.text = zombieAlive.ToString();
    }

    // increase the number of the zombies in the scene by 1. called by enemy instantiator
    public void IncreaseNumberOfZombie()
    {
        zombieAlive++;
    }

    // decrease the number of zombie in the scene. called by EnemyHealth
    public void DecreaseNumberOfZombie()
    {
        zombieAlive--;
        // if the zombie number is 0, load next level becuase the current level completed.
        // ask for bug :)
        if(zombieAlive == 0)
        {
            sceneLoader.LoadNextScene();
        }
    }


}
