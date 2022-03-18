using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    // save current Scene
    int currentScene = 0;
    
    // this gameobject is DontDestroyOnLoad it means we will not destroy after scene is
    // ended and new scene is loading.
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // the Level1 scene is scene number 1 so that we will load this scene
    public void ReloadGame()
    {
        // change the current scene to 1
        currentScene = 1;
        // after the game is stopped due to the player died the time is 0 which mean the game is froze
        // we want to change the timeScale to 1 in order the enable another game
        Time.timeScale = 1;
        // load the Scene of the level 1
        SceneManager.LoadScene(1);
    }

    // Quit Game
    public void QuitGame()
    {
        Application.Quit();
    }

    // Back to the main menu (level 0 = StartScene)
    public void BackToMainMenu()
    {
        // change the timeScale to 1 in order to show zombie animation at the begging
        Time.timeScale = 1;
        // change current scene to 0
        currentScene = 0;
        // Destroy game object because it is a DontDestroyOnLoad gameObject in the StartScene
        // we will create another DontDestroyOnLoad game object which can cause problems
        Destroy(gameObject);
        // Load StartScene
        SceneManager.LoadScene(0);
    }

    // Load Next Scene after level is completed
    public void LoadNextScene()
    {
        currentScene++;
        SceneManager.LoadScene(currentScene);
    }
}
