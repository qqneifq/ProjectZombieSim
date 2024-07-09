using System.Collections.Generic;
using UnityEngine;

public interface IBuildingChainHandler
{
    public void MoveToNext();
    public void SetBuilding(int building);
    public int GetBuildingType();
    public void AddBuilding(GameObject building);

    public List<GameObject> GetAllObjectsInQueue();
    public void Declain();
    public void SetBuildings(List<GameObject> gameObjects);
}
