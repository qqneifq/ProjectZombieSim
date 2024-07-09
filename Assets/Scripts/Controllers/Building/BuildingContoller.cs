using System.Collections.Generic;
using UnityEngine;

public class BuildingContoller : MonoBehaviour
{
    private List<(int, int)> _usedPoints;
    private Building _data;

    public Vector3 GetSize()
    {
        return _data._size;
    }

    public void SetData(Building data) { 
        _data = data;
    }
    public List<int> GetConditions()
    {
        return _data._buildingConditions;
    }
    public BuildingsConsts.BuildingIndificator GetIndificator()
    {
        return _data.getBuildingIndificator();
    }
    public Transform GetTransform()
    {
        return transform;
    }

    public void SetUsedPoints(List<(int, int)> usedPoints) { 
        _usedPoints = usedPoints;
    }
    public List<(int, int)> getUsedPoints ()
    {
        return _usedPoints;
    }
}
