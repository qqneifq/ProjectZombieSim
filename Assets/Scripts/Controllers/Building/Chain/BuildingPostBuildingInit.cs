using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPostBuildingInit : IChainPart
{
    private IBuildingChainHandler _handler;
    public BuildingPostBuildingInit(IBuildingChainHandler handler)
    {
        _handler = handler;
    }
    public void Update(double delta)
    {
       if(_handler.GetBuildingType() == (int)BuildingsConsts.BuildingIndificator.Wall)
       {
            //Do magic to create a wall
       }

        _handler.MoveToNext();
    }

}
