using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHandler : MonoBehaviour
{
    private List<int> resources;

    private void Start()
    {
        resources = new List<int>();
    }

    public void AddResource(int index, int count)
    {
        resources[index] += count;
    }

    public void AddResources(List<int> resourcesOnAdd)
    {
        for(int i = 0; i < resources.Count; i++)
        {
            AddResource(i, resourcesOnAdd[i]);
        }
    }

    public void RemoveResource(int index, int count) 
    {
        resources[index] -= count;
    }

    public void RemoveResources(List<int> resourcesOnDelete)
    {
        for(int i = 0; i < resources.Count; i++)
        {
            RemoveResource(i, resourcesOnDelete[i]);
        }
    }

    public bool IsEnoughResource(int index, int count)
    {
        return resources[index] >= count;
    }

    public bool IsEnoughResources(List<int> resourcesOnCheck) { 
        for(int i =0; i < resources.Count;i++)
        {
            if(!IsEnoughResource(i, resourcesOnCheck[i]))
            {
                return false;
            }
        }

        return true;
    }
    
}
