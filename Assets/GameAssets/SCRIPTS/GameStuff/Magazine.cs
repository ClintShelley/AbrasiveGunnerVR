using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    [SerializeField] private Renderer myMag;
    public int numberOfBullet = 12;

    void Update()
    {
        if (numberOfBullet == 0)
        {
            myMag.material.color = Color.red;
        }
    }
}
