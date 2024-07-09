using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    Transform bulletPosition;
    [SerializeField]
    float velocity = 500f;
    [SerializeField]
    float bulletLifeTime = 3f;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        CameraSync.OnCameraSwitched += SwitchCamera;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            ShootWeapon();
        }
    }

    void ShootWeapon()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        GameObject bullet = Instantiate(bulletPrefab, bulletPosition.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(ray.direction.normalized * velocity, ForceMode.Impulse);
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletLifeTime));
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }

    void SwitchCamera(GameObject camera)
    {
        cam = camera.GetComponent<Camera>();
    }
}
