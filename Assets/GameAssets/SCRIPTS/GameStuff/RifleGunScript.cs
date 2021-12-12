using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MouseLooking;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

namespace rifleGunScript
{

    public class RifleGunScript : MonoBehaviour
    {
        //Audio resources
        AudioSource audioSource;
        [SerializeField] AudioClip GunShot;
        [SerializeField] AudioClip ReloadingIN;
        [SerializeField] AudioClip ReloadingOUT;
        [SerializeField] AudioClip Dryfire;
        [SerializeField] AudioClip HitMarker;

        [SerializeField] GameObject shootPoint;

        //Units for shooting
        public float damage = 100f;
        public float range = 150f;
        public float impactForce = 80f;
        public float fireRate = 20f;
        public float maxnumberOfBullet = 10;
        public string currentAmmo;
        public Text bulletText;
        public float rate = 1;


        public ParticleSystem muzzleFlash;
        public GameObject impactEffect;

        private Coroutine _current;

        public Magazine magazine;
        public XRBaseInteractor socketInteractor;

        public void AddMagazine(XRBaseInteractable interactable)
        {
            magazine = interactable.GetComponent<Magazine>();
            audioSource.PlayOneShot(ReloadingIN);
            currentAmmo = magazine.numberOfBullet.ToString();
            bulletText.text = (currentAmmo + "/" + maxnumberOfBullet);
        }

        public void RemoveMagazine(XRBaseInteractable interactable)
        {
            magazine = null;
            audioSource.PlayOneShot(ReloadingOUT);
            bulletText.text = ("0" + "/" + maxnumberOfBullet);
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
                if (_current != null) StopCoroutine(_current);
                _current = StartCoroutine(Shoot());
            }
            else
            {
                audioSource.PlayOneShot(Dryfire);
            }
        }

        public void StopTheTrigger()
        {
            if (_current != null) StopCoroutine(_current);
        }


        //ON shoot play audio, check for enemy, and display muzzle flash and impact effect
        private IEnumerator Shoot()
        {
            while (true && magazine.numberOfBullet > 0)
            {
                audioSource.PlayOneShot(GunShot);

                magazine.numberOfBullet--;
                currentAmmo = magazine.numberOfBullet.ToString();
                bulletText.text = (currentAmmo + "/" + maxnumberOfBullet);

                muzzleFlash.Play();
                RaycastHit hit;
                if (Physics.Raycast(shootPoint.transform.position, shootPoint.transform.forward, out hit, range))
                {
                    Enemy target = hit.transform.GetComponent<Enemy>();
                    if (target != null)
                    {
                        audioSource.PlayOneShot(HitMarker);
                        target.TakeDamage(damage);
                    }

                    if (hit.rigidbody != null)
                    {
                        hit.rigidbody.AddForce(-hit.normal * impactForce);
                    }

                    GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(impactGO, 1f);

                    yield return new WaitForSeconds(1f / rate);
                }
            }
        }
    }
}


