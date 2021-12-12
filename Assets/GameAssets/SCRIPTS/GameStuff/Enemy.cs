using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{

    public GameObject damageText;

    //Enemy HP (gun does 100 dmg)
    public float health = 100f;

    void Start()
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
}
