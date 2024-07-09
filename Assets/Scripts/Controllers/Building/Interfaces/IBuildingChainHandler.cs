using System.Collections.Generic;
using UnityEngine;

public interface IBuildingChainHandler
{
    public void MoveToNext();
    public void SetBuilding(int building);
    public int GetBuildingType();
    public void AddBuilding((BuildingContoller controller, GameObject gameObject) building);

    public List<GameObject> GetAllObjectsInQueue();
    public void Declain();
}
