using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ResourceTypes;

// put on world objects
public class ResourceModel : MonoBehaviour
{
    [SerializeField]
    ResourcesEnum type;

    public static event Action<GameObject> OnDeath;
    public ResourcesEnum Type
    {
        get { return type; }
    }
}
