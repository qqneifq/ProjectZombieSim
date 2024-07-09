using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static WeaponTypes;

public class Weapon : MonoBehaviour
{

    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    Transform bulletPosition;
    [SerializeField]
    float bulletLifeTime = 3f;

    [SerializeField]
    GameObject meleeCollider;

    [SerializeField]
    GameObject shootingCam;
    [SerializeField]
    Camera cam;

    [SerializeField]
    WeaponModel currentWeapon;
    int weaponNumber;
    [SerializeField]
    WeaponModel[] weapons;

    bool isReloading = false;
    float lastShotTime;
    float reloadStartTime;

    public GameObject ShootingCam { get => shootingCam; set => shootingCam = value; }

    void CycleWeapons()
    {
        Debug.Log($"Current number {weaponNumber}");
        if (weaponNumber < weapons.Length - 1)
        {
            weaponNumber++;
        }
        else
        {
            weaponNumber = 0;
        }
        currentWeapon = weapons[weaponNumber];
        Debug.Log($"Current number {weaponNumber}");
    }
    void SetWeapon(WeaponModel w)
    {
        currentWeapon = w;
    }

    // Start is called before the first frame update
    void Start()
    {
        lastShotTime = Time.time;
        cam = shootingCam.GetComponent<Camera>();
        CameraSync.OnCameraSwitched += SwitchCamera;
        SetWeapon(weapons[weaponNumber]);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentWeapon == null)
        {
            SetWeapon(weapons[0]);
        }
        if(Input.GetKey(KeyCode.Mouse0))
        {
            if (currentWeapon.IsRanged)
            {
                if (Time.time - lastShotTime > currentWeapon.AttackInterval && currentWeapon.CurrentAmmo > 0 && !isReloading)
                {
                    RangedAttack();
                    lastShotTime = Time.time;
                }
                else if (currentWeapon.CurrentAmmo <= 0 && !isReloading)
                {
                    Reload();
                }
            }
            else
            {
                if(Time.time - lastShotTime > currentWeapon.AttackInterval)
                {
                    MeleeAttack();
                    lastShotTime = Time.time;
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            CycleWeapons();
        }
        
    }

    void MeleeAttack()
    {

        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1f))
        {
            Debug.Log($"Melee hit {hit.collider.gameObject.name}");
        }
    }

    void RangedAttack()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        Vector3 shootDirection;
        if (Physics.Raycast(ray, out hit))
        {
            shootDirection = (hit.point - bulletPosition.position).normalized;
        }
        else
        {
            shootDirection = ray.direction;
        }

        // create bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletPosition.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().useGravity = false;
        // push bullet
        bullet.GetComponent<Rigidbody>().AddForce(shootDirection * currentWeapon.Velocity, ForceMode.Impulse);
        currentWeapon.ShootAmmo();
        Debug.Log("Shoot bullet");
        // destroy after time
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletLifeTime));

    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }

    void SwitchCamera(GameObject camera)
    {
        Debug.Log("Switching shooting camera");
        ShootingCam = camera;
        cam = ShootingCam.GetComponent<Camera>();
    }



    void Reload()
    {
        isReloading = true;
        reloadStartTime = Time.time;
        StartCoroutine(CompleteReload());
        Debug.Log("Reloading");
    }
    IEnumerator CompleteReload()
    {
        yield return new WaitForSeconds(currentWeapon.ReloadTime);
        currentWeapon.ReloadAmmo();
        isReloading = false;
        Debug.Log("Reload Complete");
    }
}
