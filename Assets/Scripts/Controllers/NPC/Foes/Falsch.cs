using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falsch : MonoBehaviour
{
    [SerializeField] private Transform _face;
    [SerializeField] private float _sphereRadius = 0.2f;
    [SerializeField] private float _sphereDistance = 0.1f;
    [SerializeField] private float _speed = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = _face.forward;

        if (Physics.SphereCast(_face.position, _sphereRadius, _face.forward, out hit, _sphereDistance))
        {
            if(hit.transform.tag != "Foe" && hit.transform.GetComponent<IDestroyable>() != null)
            {
                IDestroyable wall = hit.transform.GetComponent<IDestroyable>();
                wall.RemoveHealth(0.005);
            }
        } else
        {
            transform.Translate(_face.forward * _speed * Time.deltaTime);
        }
    }
}
