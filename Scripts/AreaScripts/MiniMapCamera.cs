using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class MiniMapCamera : MonoBehaviour
    {
        // 3D text connected to the camera
        [SerializeField] GameObject ExitTxt;
        [SerializeField] GameObject EnterTxt;
        // if the player inside the woodcamera he only activate the camera
        private bool canActivate;
        // other cameras in the scene
        [SerializeField] GameObject cameraFPS;
        [SerializeField] GameObject cameraWeapon;
        [SerializeField] GameObject cameraCanon;
        [SerializeField] GameObject Weapon;
        
        // at the beggining the player is not in the woodcamera area, thus, false
        private void Start()
        {
            canActivate = false;
        }

        // every time the player inside the trigger he can activate the camera
        private void OnTriggerStay(Collider other)
        {
            canActivate = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                ActivateCamera();
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                DeActivateCamera();
            }

        }

        public void ActivateCamera()
        {
            // acsess to camera and activating the canon camera
            GetComponentInChildren<Camera>().enabled = true;
            cameraFPS.GetComponent<Camera>().enabled = false;
            cameraWeapon.SetActive(false);
            cameraCanon.SetActive(false);
            // defuse controller options
            FirstPersonController controller = FindObjectOfType<FirstPersonController>();
            controller.m_WalkSpeed = 0f;
            controller.m_MouseLook.XSensitivity = 0f;
            controller.m_MouseLook.YSensitivity = 0f;
            Weapon.SetActive(false);
            EnterTxt.SetActive(false);
            ExitTxt.SetActive(true);
        }

        public void DeActivateCamera()
        {

            // acsess to camera and deactivating the canon camera and activating other cameras
            GetComponentInChildren<Camera>().enabled = false;
            cameraFPS.GetComponent<Camera>().enabled = true;
            cameraWeapon.SetActive(true);
            cameraCanon.SetActive(true);

            // activate controller options
            FirstPersonController controller = FindObjectOfType<FirstPersonController>();
            controller.m_WalkSpeed = 2.5f;
            controller.m_MouseLook.XSensitivity = 2f;
            controller.m_MouseLook.YSensitivity = 2f;
            Weapon.SetActive(true);
            EnterTxt.SetActive(true);
            ExitTxt.SetActive(false);
        }
    }
}
