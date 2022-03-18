using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathHandler : MonoBehaviour
{
    // GUI canvas displaying
    [SerializeField] Canvas gameOverCanvas;
    MethorLauncher methorLauncher;

    // caching for methor launcher if the level is level2
    private void Start()
    {
        gameOverCanvas.enabled = false;
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            methorLauncher = FindObjectOfType<MethorLauncher>();
        }
    }

    // called by PlayerHealth
    public void HandleDeath()
    {
        // activate canvas
        gameOverCanvas.enabled = true;
        // stop time and show cursor
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // disable the controller hid cursor
        var firstPersonControllerCamera = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        var mouseLook = firstPersonControllerCamera.m_MouseLook;
        mouseLook.SetCursorLock(false);
        firstPersonControllerCamera.enabled = false;

        // stop methor if the level is level2
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            methorLauncher.StopMethor();
        }

    }
}
