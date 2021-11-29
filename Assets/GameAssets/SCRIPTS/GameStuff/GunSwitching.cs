using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using primaryGunScript;

public class GunSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;
    private GunScript gunScript;

    // Start is called before the first frame update
    void Start()
    {
        gunScript = GameObject.FindObjectOfType<GunScript>();
        selectWeapon();
    }

    // Update is called once per frame
    void Update()
    {

        //int previousSelectedWeapon = selectedWeapon;
        //if (gunScript.isReloading == false)
        //{
        //    if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        //    {
        //        if (selectedWeapon >= transform.childCount - 1)
        //            selectedWeapon = 0;
        //        else
        //            selectedWeapon++;
        //    }
        //    if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        //    {
        //        if (selectedWeapon <= 0)
        //            selectedWeapon = transform.childCount - 1;
        //        else
        //            selectedWeapon--;
        //    }

        //    if (previousSelectedWeapon != selectedWeapon)
        //    {
        //        selectWeapon();
        //    }
        //}
    }
    //for each gun set its units to 0 - X number and set active
    void selectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
                else
                    weapon.gameObject.SetActive(false);
                i++;
        }
    }
}
