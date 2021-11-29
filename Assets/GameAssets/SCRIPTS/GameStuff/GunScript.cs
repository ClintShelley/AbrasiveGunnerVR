using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MouseLooking;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

namespace primaryGunScript
{

    public class GunScript : MonoBehaviour
    {
        //Audio resources
        AudioSource audioSource;
        [SerializeField] AudioClip GunShot;
        [SerializeField] AudioClip ReloadingIN;
        [SerializeField] AudioClip ReloadingOUT;
        [SerializeField] AudioClip Dryfire;

        //Units for shooting
        public float damage = 100f;
        public float range = 150f;
        public float impactForce = 80f;
        public float fireRate = 20f;
        public float maxnumberOfBullet = 12;
        public string currentAmmo;
        public Text leftbulletText;
        public Text rightbulletText;


        public ParticleSystem muzzleFlash;
        public GameObject impactEffect;


        public Magazine magazine;
        public XRBaseInteractor socketInteractor;

        public void AddMagazine(XRBaseInteractable interactable)
        {
            magazine = interactable.GetComponent<Magazine>();
            audioSource.PlayOneShot(ReloadingIN);
            currentAmmo = magazine.numberOfBullet.ToString();
            leftbulletText.text = (currentAmmo + " / " + maxnumberOfBullet + " Bullets");
            rightbulletText.text = (currentAmmo + " / " + maxnumberOfBullet + " Bullets");
        }

        public void RemoveMagazine(XRBaseInteractable interactable)
        {
            magazine = null;
            audioSource.PlayOneShot(ReloadingOUT);
            leftbulletText.text = ("0" + " / " + maxnumberOfBullet + " Bullets");
            rightbulletText.text = ("0" + " / " + maxnumberOfBullet + " Bullets");
        }


        private void Start()
        {
            audioSource = GetComponent<AudioSource>();

            socketInteractor.onSelectEntered.AddListener(AddMagazine);
            socketInteractor.onSelectExited.AddListener(RemoveMagazine);
        }

        public void PullTheTrigger()
        {

            if (magazine && magazine.numberOfBullet > 0)
            {
                Shoot();
            }
            else
            {
                audioSource.PlayOneShot(Dryfire);
            }
        }


        //ON shoot play audio, check for enemy, and display muzzle flash and impact effect
        void Shoot()
        {
            audioSource.PlayOneShot(GunShot);

            magazine.numberOfBullet--;
            currentAmmo = magazine.numberOfBullet.ToString();
            leftbulletText.text = (currentAmmo + " / " + maxnumberOfBullet + " Bullets");
            rightbulletText.text = (currentAmmo + " / " + maxnumberOfBullet + " Bullets");

            muzzleFlash.Play();
            RaycastHit hit;
            if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, range))
            {
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
