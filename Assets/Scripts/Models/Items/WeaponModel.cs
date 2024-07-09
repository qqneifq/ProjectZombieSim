using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WeaponTypes;

[System.Serializable]
public class WeaponModel
{
    [SerializeField]
    WeaponType weapon;
    [SerializeField]
    bool isRanged;
    [SerializeField]
    int currentAmmo;
    [SerializeField]
    int maxAmmo;
    [SerializeField]
    float reloadTime;
    [SerializeField]
    float attackInterval;
    [SerializeField]
    float velocity;

    public bool IsRanged
    {
        get { return isRanged; }
    }
    public float ReloadTime
    {
        get { return reloadTime; }
    }
    public float AttackInterval
    {
        get { return attackInterval; }
    }
    public int CurrentAmmo
    {
        get { return currentAmmo; }
    }

    public WeaponModel()
    {

    }

    public WeaponModel(WeaponType w, int ammo, float reload, float interval, bool ranged)
    {
        weapon = w;
        isRanged = ranged;
        maxAmmo = ammo;
        currentAmmo = maxAmmo;
        reloadTime = reload;
        attackInterval = interval;
    }

    public void ReloadAmmo()
    {
        if(isRanged)
        {
            currentAmmo = maxAmmo;
        }
    }
    
    public void ShootAmmo()
    {
        if(isRanged && currentAmmo > 0)
        {
            currentAmmo--;
        }
    }


}
