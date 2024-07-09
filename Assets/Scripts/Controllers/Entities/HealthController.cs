using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ResourceTypes;

public class HealthController : MonoBehaviour
{
    public static event Action<ResourcesEnum, int> OnResourceHit;
    // Start is called before the first frame update
    void Start()
    {
        Bullet.OnBulletHit += TargetHitHandler;
        WeaponController.OnHit += TargetHitHandler;
        HealthModel.OnDeath += DestroyOnDeath;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TargetHitHandler(GameObject o, float d)
    {
        if (o.GetComponent<HealthModel>() != null)
        {
            o.GetComponent<HealthModel>().RemoveHealth(d);
            
        }
        if (o.GetComponent<ResourceModel>() != null)
        {
            OnResourceHit?.Invoke(o.GetComponent<ResourceModel>().Type, (int)d);
        }
    }

    void DestroyOnDeath(GameObject go)
    {
        Destroy(go);
    }
}
