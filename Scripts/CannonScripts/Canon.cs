using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityStandardAssets.Characters.FirstPerson
{


    public class Canon : MonoBehaviour
    {
        [SerializeField] GameObject redReticle;

        AudioSource audioSource;
        [SerializeField] GameObject InstructionTxt;
        [SerializeField] GameObject ExitTxt;
        [SerializeField] GameObject EnterTxt;
        [SerializeField] GameObject hitEffect;
        [SerializeField] Quaternion startQuaternion;
        [SerializeField] GameObject CanonBall;
        [SerializeField] Transform CanPlacerForBall;
        private float fireFactor = 750f;
        private float rotateFactor = 50f;
        private bool canActivate;
        [SerializeField] GameObject cameraFPS;
        [SerializeField] GameObject cameraWeapon;
        [SerializeField] GameObject cameraWood;
        [SerializeField] GameObject Weapon;
        [SerializeField] float FireRate = 1f;
        bool canShoot;
        bool canRotate;
        [SerializeField] AudioClip canonShot;
        [SerializeField] AudioClip ballBomb;

        // by the start inital the shoot and the activate for the canon to false
        private void Start()
        {
            canShoot = false;
            canActivate = false;
            audioSource = GetComponent<AudioSource>();
        }

        // check for totaion and shooting
        private void Update()
        {
            if(canRotate)
            {
                RotateCanon();
            }
            if(canShoot)
            {
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    StartCoroutine(ShootOneTime());
                }
                
            }
        }

        IEnumerator ShootOneTime()
        {
            canShoot = false;
            // check for boundries
            if(transform.rotation.y > 0.8f)
            {
                // instaniating canon ball 
                audioSource.PlayOneShot(canonShot);
                GameObject Ball = Instantiate(CanonBall, CanPlacerForBall.position, Quaternion.identity);
                Ball.GetComponent<Rigidbody>().AddForce(CanPlacerForBall.transform.right * fireFactor);
                Ball.GetComponent<CanonBall>().SetEffect(hitEffect);
                Ball.GetComponent<CanonBall>().SetSound(ballBomb, audioSource);
            }


            yield return new WaitForSeconds(FireRate);
            canShoot = true;
        }

        // while the player is inside the canon area he is possible to activate it by pressing E
        // while the player is inside the canon area he is possible to DEactivate it by pressing Q
        private void OnTriggerStay(Collider other)
        {
            canActivate = true;
            if(Input.GetKey(KeyCode.E))
            {
                ActivateCanon();
            }
            if(Input.GetKey(KeyCode.Q))
            {
                DeActivateCanon();
            }
        }

        // rotate the cannon by pressing the arrows
        private void RotateCanon()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(Vector3.down * rotateFactor * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(Vector3.up * rotateFactor * Time.deltaTime);
            }
        }

        // adjust all the parametr to set the canon active
        public void ActivateCanon()
        {
            // control the canon behaivior = options provoked
            canShoot = true;
            canRotate = true;

            // acsess to camera and activating the canon camera
            GetComponentInChildren<Camera>().enabled = true;
            cameraFPS.GetComponent<Camera>().enabled = false;
            cameraWeapon.SetActive(false);
            cameraWood.SetActive(false);
            // defuse controller options
            FirstPersonController controller = FindObjectOfType<FirstPersonController>();
            controller.m_WalkSpeed = 0f;
            controller.m_MouseLook.XSensitivity = 0f;
            controller.m_MouseLook.YSensitivity = 0f;
            Weapon.SetActive(false);
            EnterTxt.SetActive(false);
            ExitTxt.SetActive(true);
            InstructionTxt.SetActive(true);
            redReticle.SetActive(false);
        }

        public void DeActivateCanon()
        {
            // control the canon behaivior = options ignored
            canShoot = false;
            canRotate = false;

            // acsess to camera and deactivating the canon camera and activating other cameras
            GetComponentInChildren<Camera>().enabled = false;
            cameraFPS.GetComponent<Camera>().enabled = true;
            cameraWeapon.SetActive(true);
            cameraWood.SetActive(true);

            // activate controller options
            FirstPersonController controller = FindObjectOfType<FirstPersonController>();
            controller.m_WalkSpeed = 2.5f;
            controller.m_MouseLook.XSensitivity = 2f;
            controller.m_MouseLook.YSensitivity = 2f;
            Weapon.SetActive(true);
            EnterTxt.SetActive(true);
            ExitTxt.SetActive(false);
            InstructionTxt.SetActive(false);
            redReticle.SetActive(true);
            // inital rotaion for next time
            transform.rotation = startQuaternion;
        }
    }
}