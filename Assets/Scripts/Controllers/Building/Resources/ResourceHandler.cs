using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHandler : MonoBehaviour
{
    [SerializeField] private List<int> _resources;
    [SerializeField] private List<int> _maxResources;
    //-1 - we can have unlimited count of resource, 0 - we cant have this type of resources, 1+ - we can have as much as we need
    public static event Action<int, int, int> OnResourceChange;

    //initialize UI
    private void Start()
    {
        for(int i = 0; i < _resources.Count; i++)
        {
            OnResourceChange?.Invoke(i, _resources[i], _maxResources[i]);
        }
    }

    public void PlusResources()
    {
        _resources.Add(0);
    }

    public void PlusMaxResources()
    {
        _maxResources.Add(0);
    }
    public void AddResource(int index, int count)
    {
        if (_resources[index] + count > _maxResources[index] && _maxResources[index] > 0)
        {
            _resources[index] = _maxResources[index];
        }
        else
        {
            _resources[index] += count;
        }
        OnResourceChange?.Invoke(index, _resources[index], _maxResources[index]);
    }

    public void AddResources(List<int> resourcesOnAdd)
    {
        for(int i = 0; i < _resources.Count; i++)
        {
            AddResource(i, resourcesOnAdd[i]);
        }
    }

    public void RemoveResource(int index, int count, int number) 
    {
        _resources[index] -= count*number;
    }

    public void RemoveResources(List<int> resourcesOnDelete, int number)
    {
        for(int i = 0; i < _resources.Count; i++)
        {
            RemoveResource(i, resourcesOnDelete[i], number);
        }
    }

    public bool IsEnoughResource(int index, int count, int number)
    {
        return _resources[index] >= count*number;
    }

    public bool IsEnoughResources(List<int> resourcesOnCheck, int number) { 
        for(int i =0; i < _resources.Count;i++)
        {
            if(!IsEnoughResource(i, resourcesOnCheck[i], number))
            {
                return false;
            }
        }

        return true;
    }
    
    public void AddResourceToMax(int index, int count, int number)
    {
        _maxResources[index] += count*number;
    }

    public void AddResourcesToMax(List<int> resourcesOnAdd, int number)
    {
        for(int i = 0; i < _resources.Count; i++)
        {
            AddResourceToMax(i, resourcesOnAdd[i], number);
        }
    }

}
