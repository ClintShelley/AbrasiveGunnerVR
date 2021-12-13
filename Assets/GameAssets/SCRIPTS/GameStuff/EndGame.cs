using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    //protected GameObject player;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name =="XR Rig")
        {
            SceneManager.LoadScene("EndScene");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.Find("XR Rig");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
