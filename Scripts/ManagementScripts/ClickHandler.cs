using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{

    // because sceneloader is DontDestroyOnLoad object we need a class to comunicate between the SceneLoader method
    // to the buttons every single scene. thus the ClickHandler has a SceneLoader component which makes caching for
    // SceneLoad of DontDestroyOnLoad every scene and he called his methods

    SceneLoader sceneLoader;

    // caching for SceneLoader script
    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();   
    }

    // when the play again button is clicked ClickHandler calls SceneLoader method which run another time the game
    public void HandlePlayAgain()
    {
        sceneLoader.ReloadGame();
    }

    // when the main menu button is clicked ClickHandler calls SceneLoader method which back to main menu
    public void BackToMenu()
    {
        sceneLoader.BackToMainMenu();
    }

    // when the quit button is clicked ClickHandler calls SceneLoader method which Quit the game
    public void Quit()
    {
        sceneLoader.QuitGame();
    }
}
