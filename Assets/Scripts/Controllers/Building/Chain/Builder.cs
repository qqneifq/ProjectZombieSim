using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour, IBuildingChainHandler
{
    [SerializeField] private Transform _face;
    [SerializeField] private LayerMask _groundMask;

    [SerializeField] private float _raycastField;

    private Floor _floorController;
    private BuildingsFactory _buildingsFactory;

    private ResourceHandler _resourceHandler;

    private List<GameObject> _buildings;
    private List<IChainPart> _chain;

    private int _buildingType;
    private int _index;

    void Start()
    {
        _floorController = FindObjectOfType<Floor>();
        _resourceHandler = FindObjectOfType<ResourceHandler>();
        _buildingsFactory = FindObjectOfType<BuildingsFactory>();

        _chain = new List<IChainPart>();
        _buildings = new List<GameObject> ();

        _index = 0;
        _buildingType = 0;

        BuildingStart start = new BuildingStart(this, _floorController);
        BuildingChoose choose = new BuildingChoose(this, _face, _groundMask, _buildingsFactory, _raycastField, _floorController.GetDelta());
        BuildingCreating create = new BuildingCreating(this, _floorController, _face, _groundMask, _buildingsFactory, _raycastField, _floorController.GetDelta());
        BuildingPostBuildingInit postInitOfBuildings = new BuildingPostBuildingInit(this, _floorController, _buildingsFactory);

        _chain.Add(start);
        _chain.Add(choose);
        _chain.Add(create);
        _chain.Add(postInitOfBuildings);
    }

    public void MoveToNext()
    {
        _index = (_index + 1) % _chain.Count;
        if(_index == 0 && _buildings.Count > 0)
        {
            _floorController.DestroyBuildingMap();
            if (!_resourceHandler.IsEnoughResources(_buildingsFactory.Get((BuildingsConsts.BuildingIndificator)_buildingType)._buildingConditions, _buildings.Count))
            {
                foreach (var obj in _buildings)
                {
                    BuildingContoller bc = obj.GetComponent<BuildingContoller>();
                    _floorController.ReleaseBuildingPoints(bc.getUsedPoints());

                    GameObject.Destroy(obj.gameObject);
                }
            } else
            {
                _resourceHandler.RemoveResources(_buildingsFactory.Get((BuildingsConsts.BuildingIndificator)_buildingType)._buildingConditions, _buildings.Count);
            }
            _buildingType = 0;
            _buildings.Clear();
        }
    }

    void Update()
    {
        _chain[_index].Update(Time.deltaTime);
    }

    public void SetBuilding(int type)
    {
        _buildingType = type;
    }

    public int GetBuildingType()
    {
        return _buildingType;
    }

    public void AddBuilding(GameObject building)
    {
        _buildings.Add(building);
    }

    public List<GameObject> GetAllObjectsInQueue()
    {
        List<GameObject> list = new List<GameObject>();
        foreach (var obj in _buildings)
        {
            list.Add(obj.gameObject);
        }
        return list;
    }

    public void Declain()
    {
        _floorController.DestroyBuildingMap();
        _buildingType = 0;

        foreach (var obj in _buildings)
        {
            BuildingContoller bc = obj.GetComponent<BuildingContoller>();

            _floorController.ReleaseBuildingPoints(bc.getUsedPoints());

            GameObject.Destroy(obj.gameObject);
        }

        _buildings.Clear();
        _index = 0;
    }

    public void SetBuildings(List<GameObject> gameObjects)
    {
        _buildings = gameObjects;
    }
}
