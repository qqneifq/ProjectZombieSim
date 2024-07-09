using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static BuildingsConsts;

public class BuildingStart : IChainPart
{
    private Floor _floor;
    private IBuildingChainHandler _handler;

    public BuildingStart(IBuildingChainHandler handler, Floor floor)
    {
        _floor = floor;
        _handler = handler; 
    }

    public void Update(double delta)
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            _floor.CreateBuildingMap();
            _handler.MoveToNext();
        }
    }
}
