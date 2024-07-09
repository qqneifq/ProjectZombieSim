using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        Bullet.OnBulletHit += TargetHitHandler;
        Weapon.OnHit += TargetHitHandler;
        HealthModel.OnDeath += DestroyOnDeath;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TargetHitHandler(GameObject o, float d)
    {
        var hModel = o.GetComponent<HealthModel>();
        if (hModel != null)
        {
            hModel.RemoveHealth(d);
        }
    }

    void DestroyOnDeath(GameObject go)
    {
        Destroy(go);
    }
}
