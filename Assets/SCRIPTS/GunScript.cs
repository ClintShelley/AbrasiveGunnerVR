using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MouseLooking;
using UnityEngine.UI;
using TMPro;

namespace primaryGunScript
{

    public class GunScript : MonoBehaviour
    {
        //Audio resources
        AudioSource audioSource;
        [SerializeField] AudioClip GunShot;
        [SerializeField] AudioClip Reloading;

        //Units for shooting
        public float damage = 100f;
        public float range = 150f;
        public float impactForce = 80f;
        public float fireRate = 20f;

        public int maxAmmo = 5;
        private int currentAmmo;
        public float reloadTime = 8f;
        public bool isReloading = false;

        public Animator animator;

        public Camera FPSCam;
        public ParticleSystem muzzleFlash;
        public GameObject impactEffect;

        private float nextTimeToFire = 0f;

        public TextMeshProUGUI ammoDisplay;


        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            currentAmmo = maxAmmo;
        }

        void onEnable()
        {
            isReloading = false;
            animator.SetBool("Reloading", false);
        }

        // Update is called once per frame
        void Update()
        {
            ammoDisplay.text = (currentAmmo.ToString() + "/" + maxAmmo.ToString() + " Bullets");

            if (isReloading)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
            {
                StartCoroutine(Reload());
            }

            if (currentAmmo <= 0)
            {
                StartCoroutine(Reload());
                return;
            }

            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && currentAmmo > 0)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }

        //Reload when ammo is out and wait xtime
        IEnumerator Reload()
        {
            isReloading = true;
            audioSource.PlayOneShot(Reloading);
            animator.SetBool("Reloading", true);

            yield return new WaitForSeconds(reloadTime);

            animator.SetBool("Reloading", false);
            currentAmmo = maxAmmo;
            isReloading = false;
        }


        //ON shoot DO THESE
        void Shoot()
        {
            audioSource.PlayOneShot(GunShot);

            currentAmmo--;

            muzzleFlash.Play();
            RaycastHit hit;
            if (Physics.Raycast(FPSCam.transform.position, FPSCam.transform.forward, out hit, range))
            {
                //Debug.Log(hit.transform.name);

                Enemy target = hit.transform.GetComponent<Enemy>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }

                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 1f);
            }
        }
    }
}
