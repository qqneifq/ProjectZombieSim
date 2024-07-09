using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ResourceTypes;

public class ResourceController : MonoBehaviour
{
    [SerializeField]
    GameObject resourceHandler;
    ResourceHandler rModel;
    void Start()
    {
        rModel = resourceHandler.GetComponent<ResourceHandler>();
        HealthController.OnResourceHit += ResourceHitHandler;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ResourceHitHandler(ResourcesEnum type, int damage)
    {
        rModel.AddResource(Index(type), 5);
    }
}
