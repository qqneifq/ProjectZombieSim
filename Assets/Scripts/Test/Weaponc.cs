using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WeaponTypes;

public class Weaponc : MonoBehaviour
{
    [SerializeField]
    WeaponModel[] weapons;
    WeaponModel currentWeapon;
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    Transform bulletPosition;
    [SerializeField]
    float velocity = 500f;
    [SerializeField]
    float bulletLifeTime = 3f;
    Camera cam;
    [SerializeField]
    GameObject camera;

    bool isReloading = false;
    float lastShotTime;
    [SerializeField]
    float shotInterval;

    [SerializeField]
    float reloadTime = 2f;
    float reloadStartTime;
    WeaponType weaponType;
}
