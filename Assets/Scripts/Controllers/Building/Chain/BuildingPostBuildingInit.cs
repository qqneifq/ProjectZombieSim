using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPostBuildingInit : IChainPart
{
    private IBuildingChainHandler _handler;
    private Floor _floor;
    private BuildingsFactory _factory;

    private FencePostProcessor _postProcessor;

    public BuildingPostBuildingInit(IBuildingChainHandler handler, Floor floor, BuildingsFactory buildingsFactory)
    {
        _handler = handler;
        _floor = floor;
        _factory = buildingsFactory;

        _postProcessor = new FencePostProcessor(_floor, _factory);
    }
    public void Update(double delta)
    {
       if(_handler.GetBuildingType() == (int)BuildingsConsts.BuildingIndificator.Wall)
       {
            _handler.SetBuildings(_postProcessor.Process(_handler.GetAllObjectsInQueue()));
       }

        _handler.MoveToNext();
    }

}
