using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    AudioSource enemyAudio;
    //public AudioClip EnemyHit;

    private Coroutine dpsCo;
    private float nextAtk;
    private float Cooldown = 0.5f;

    public GameObject damageText;

    //Enemy HP (gun does 100 dmg)
    public float health = 100f;
    public float deltDamage = 10f;

    private void Start()
    {
       // GameObject obj = GameObject.Find("EnemyAudioHolder");

        //AudioSource enemyAudio = obj.GetComponent<AudioSource>();
    }

    private void Update()
    {

    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
        if (damageText && health > 0)
        {
            floatingDamage();
        }
    }

    void floatingDamage()
    {
        var go = Instantiate(damageText, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = health.ToString();
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public void startDealDamage()
    {
        if (nextAtk > 0)
        {
            nextAtk -= Time.deltaTime;
        }
        else if (nextAtk <= 0)
        {
            if (dpsCo != null) StopCoroutine(dpsCo);
            dpsCo = StartCoroutine(dealDamage());
            nextAtk = Cooldown;
        }
    }

    public void stopDealDamage()
    {
        if (dpsCo != null) StopCoroutine(dpsCo);
    }

    private IEnumerator dealDamage()
    {
        //enemyAudio.PlayOneShot(EnemyHit, 1f);
        FindObjectOfType<PlayerHealth>().damageMe(25f);
        if (dpsCo != null) StopCoroutine(dpsCo);
        yield return new WaitForSeconds(5f);
    }
}
