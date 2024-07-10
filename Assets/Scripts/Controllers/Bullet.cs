using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float damage;
    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    public static event Action<GameObject, float> OnBulletHit;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //here you can choose what to do with bullets hitting something. naprimer if collision.collider eto vrag,
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Hit {collision.collider.gameObject.name}");
        if(collision.collider.gameObject.name != "Weapon" && collision.collider.gameObject.name != "MeleeCollider")
        {
            Destroy(gameObject);
        }

        IDestroyable destroyableScript = collision.collider.gameObject.GetComponent<IDestroyable>();

        if (destroyableScript != null ) { 
            destroyableScript.RemoveHealth(damage);
        }
            OnBulletHit?.Invoke(collision.collider.gameObject, damage);
        //collision.collider.gameObject.getComponent<Enemy>.Health--;
    }
}
