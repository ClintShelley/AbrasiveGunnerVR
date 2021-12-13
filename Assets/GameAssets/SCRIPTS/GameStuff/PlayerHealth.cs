using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Text healthText;
    public float MyHealth = 100;
    AudioSource audioSource;
    [SerializeField] AudioClip Hurting;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = (MyHealth.ToString());
        if(MyHealth == 0)
        {
            SceneManager.LoadScene("DeadScene");
        }
    }
    public void damageMe(float amount)
    {
        audioSource.PlayOneShot(Hurting);
        MyHealth -= amount;
    }
}
