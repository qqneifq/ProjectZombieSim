using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Hit {collision.collider.gameObject.name}");
        if(collision.collider.gameObject.name != "Weapon" && collision.collider.gameObject.name != "MeleeCollider")
        {
            Destroy(gameObject);
        }
    }
}
