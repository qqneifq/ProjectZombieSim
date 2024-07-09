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

    private List<(BuildingContoller building, GameObject gameObject)> _buildings;
    private List<IChainPart> _chain;

    private int _buildingType;
    private int _index;

    void Start()
    {
        _floorController = FindObjectOfType<Floor>();
        _resourceHandler = FindObjectOfType<ResourceHandler>();
        _buildingsFactory = FindObjectOfType<BuildingsFactory>();

        _chain = new List<IChainPart>();
        _buildings = new List<(BuildingContoller building, GameObject gameObject)> ();

        _index = 0;
        _buildingType = 0;

        BuildingStart start = new BuildingStart(this, _floorController);
        BuildingChoose choose = new BuildingChoose(this, _face, _groundMask, _buildingsFactory, _raycastField, _floorController.getDelta());
        BuildingCreating create = new BuildingCreating(this, _floorController, _face, _groundMask, _buildingsFactory, _raycastField, _floorController.getDelta());
        BuildingPostBuildingInit postInitOfBuildings = new BuildingPostBuildingInit(this);

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
            if (!_resourceHandler.IsEnoughResources(_buildings[0].building.GetConditions(), _buildings.Count))
            {
                foreach (var obj in _buildings)
                {
                    _floorController.ReleaseBuildingPoints(obj.building.getUsedPoints());

                    GameObject.Destroy(obj.gameObject);
                }
            } else
            {
                _resourceHandler.RemoveResources(_buildings[0].building.GetConditions(), _buildings.Count);
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

    public void AddBuilding((BuildingContoller controller, GameObject gameObject) building)
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
        _buildingType = 0;

        foreach (var obj in _buildings)
        {
            _floorController.ReleaseBuildingPoints(obj.building.getUsedPoints());

            GameObject.Destroy(obj.gameObject);
        }

        _buildings.Clear();
        _index = 0;
    }
}
