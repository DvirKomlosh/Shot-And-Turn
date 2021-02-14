using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    public Transform gunHolder;
    public Gun startingGun;
    Gun equipedGun;

    void Start() 
    {
        if (startingGun != null) 
        {
            Equip(startingGun);
        }
    }

    void Equip(Gun gunToEquip) 
    {
        if (equipedGun != null)
            Destroy(equipedGun);
        equipedGun = Instantiate(gunToEquip , gunHolder.position , gunHolder.rotation);
        equipedGun.transform.parent = gunHolder;
        equipedGun.SetActive();
    }

    public void shoot() 
    {
        if (equipedGun != null)
            equipedGun.shoot();
    }

}
