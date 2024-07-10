using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
    [SerializeField]
    float damage;

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
    public float Velocity
    {
        get { return velocity; }
    }
    public float Damage 
    {
        get
        {
            return damage; 
        } 
    }
    public int MaxAmmo
    {
        get 
        { 
            return maxAmmo;
        }
    }
    public WeaponType Type
    {
        get { return weapon; }
    }

    public WeaponModel()
    {

    }

    public WeaponModel(WeaponType weapon, bool isRanged, int currentAmmo, int maxAmmo, float reloadTime, float attackInterval, float velocity, int damage)
    {
        this.weapon = weapon;
        this.isRanged = isRanged;
        this.currentAmmo = currentAmmo;
        this.maxAmmo = maxAmmo;
        this.reloadTime = reloadTime;
        this.attackInterval = attackInterval;
        this.velocity = velocity;
        this.damage = damage;
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
