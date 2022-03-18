using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // audio source compnent
    AudioSource audioSource;
    // particle system for the hit from the barrel effect
    [SerializeField] ParticleSystem muzzleFlash;
    // hit effect for hitting things
    [SerializeField] GameObject hitEffect;
    // camera for raycasting
    [SerializeField] Camera FPCamera;

    // weapon details
    [SerializeField] float range = 100f;
    [SerializeField] float timeBetweenShots = 0.15f;
    [SerializeField] float reloadTime = 2f;
    [SerializeField] float stabTime = 0.5f;
    [SerializeField] float ammointationTime = 3f;
    [SerializeField] int ammointationReward = 1;
    // bool can shoot alowed when there is no other action that happend
    private bool canShoot;
    private bool canReload;
    private bool canAmmoination;

    // anno slot of the weapon
    [SerializeField] Ammo ammoSlot;

    // AudioSounds
    [SerializeField] AudioClip shot;
    [SerializeField] AudioClip realod;

    // caching for audiosource component and canshot is true because the weapon just created
    void Start()
    {
        canReload = true;
        canShoot = true;
        canAmmoination = true;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // every single frame check for actions.
    void Update()
    {
        CheckForShootingSignal();
        CheckForReload();
        CheckForStab();
        CheckForAmmoination();
    }

    private void CheckForShootingSignal()
    {
        // if there is a click on the left button of the mouse and there is no other actios that happend
        // that mean that canShhot = true, Shoot.
        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        // if there is enough bullrt in the magazine
        if (ammoSlot.GetCurrentAmmo() > 0)
        {
            // canShoot = false because we just took shot
            canShoot = false;
            canReload = false;
            canAmmoination = false;
            // play the muzzle Flash particle system
            PlayMuzzleFlash();
            // Process raycast to show where i just hit
            ProcessRaycast();
            // Show anumation of Shooting
            GetComponent<Animator>().SetTrigger("ShootCarbine");
            // Play sound of shot
            audioSource.PlayOneShot(shot);
            // Reduce the ammo in the magazine by 1
            ammoSlot.ReduceAmmoAmount();
        }


        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
        canReload = true;
        canAmmoination = true;
    }

    // play the muzzle Flash particle system
    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        // hit Raycast is used to save the information of the hit
        RaycastHit hit;
        // Physics.Raycast return true if we hit something. therfore we recive hit information
        // of the hit that we just made
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            // show effect of hitting in the object
            CreateHitImpact(hit);
            // if the hit object is Enemy we want do dcrease his Health.
            // otherwise the target is not an enemy so its dont have a Enemyhealth component
            // and than the target will be null and we will just return
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) { return; }
            target.TakeDamage(40f);
        }
        // we didnt hit anything
        else { return; }
    }

    // instatniate hit effect
    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 0.1f);
    }

    // if we press on E && there is ammo in the Equipment and we allowd to reload 
    // than we will start ther Relload Sequence
    private void CheckForReload()
    {
        if (Input.GetKeyDown(KeyCode.R) && ammoSlot.GetAmmoTotal() > 0 && canReload)
        {
            canShoot = false;
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        // ReloadMagzine Method return true if we can reload the maagazine and false otherwise
        // if we can reload the magazine we will shot to animation and play the sound
        if (ammoSlot.ReloadMagazine())
        {
            GetComponent<Animator>().SetTrigger("ReloadCarbine");
            audioSource.PlayOneShot(realod);
        }

        yield return new WaitForSeconds(reloadTime);
        canShoot = true;
        canReload = true;
        canAmmoination = true;
    }

    private void CheckForStab()
    {
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            canShoot = false;
            StartCoroutine(Stab());
        }
    }
    private IEnumerator Stab()
    {
        GetComponent<Animator>().SetTrigger("StabCarbine");
        yield return new WaitForSeconds(stabTime);
        canShoot = true;
        canReload = true;
        canAmmoination = true;
    }

    private void CheckForAmmoination()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1) && canAmmoination)
        {
            canShoot = false;
            canReload = false;
            canAmmoination = false;
            StartCoroutine(Ammoination());
        }
    }

    private IEnumerator Ammoination()
    {
        GetComponent<Animator>().SetTrigger("AmmoinationCarbine");
        ammoSlot.AddAmmo(ammointationReward);
        yield return new WaitForSeconds(ammointationTime);
        canShoot = true;
        canReload = true;
        canAmmoination = true;
    }

   
    
    
}
