using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Building : PlaceableObject
{
    public BuildingsConsts.BuildingIndificator _buildingIndificator;
    public List<int> _buildingConditions;

    public Building() { }

    public Building(BuildingsConsts.BuildingIndificator buildingIndificator, List<int> conditions, GameObject gameBody, string name)
    {
        _buildingConditions = conditions;
        _buildingIndificator = buildingIndificator;
        _gameBody = gameBody;
        _name = name;
    }
    
    public BuildingsConsts.BuildingIndificator getBuildingIndificator() { return _buildingIndificator; }
    public void setBuildingIndificator(BuildingsConsts.BuildingIndificator buildingIndificator) { _buildingIndificator = buildingIndificator; }
}
