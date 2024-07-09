using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCreating : IChainPart
{
    private IBuildingChainHandler _handler;
    private Floor _floor;

    private Transform _face;
    private LayerMask _groundMask;

    private BuildingsFactory _buildingsFactory;

    private BuildingContoller _building;
    private BuildingContoller _buildingContoller;
    private GameObject _buildingGO = null;

    private float _raycastField;
    private float _fieldDelta;

    public BuildingCreating(IBuildingChainHandler handler, Floor floor, Transform face, LayerMask groundMask, BuildingsFactory buildingsFactory, float raycastField, float fieldDelta)
    {
        _handler = handler;
        _floor = floor;
        _face = face;
        _groundMask = groundMask;
        _buildingsFactory = buildingsFactory;
        _raycastField = raycastField;
        _fieldDelta = fieldDelta;
    }

    public void Update(double delta)
    {
        if (_building == null)
        {
            Building building = _buildingsFactory.Get((BuildingsConsts.BuildingIndificator)_handler.GetBuildingType());
            _buildingGO = GameObject.Instantiate(building._gameBody);

            _building = _buildingGO.GetComponent<BuildingContoller>();
            _buildingGO.GetComponentInChildren<BoxCollider>().enabled = false;
        }

        //init
        RaycastHit hit;
        Vector3 fwd = _face.forward;

        Debug.DrawRay(_face.position + fwd * 1, fwd, Color.blue);

        //Move
        if (Physics.Raycast(_face.position, fwd, out hit, _raycastField, _groundMask))
        {
            _buildingGO.transform.position = new Vector3(hit.point.x - hit.point.x % _fieldDelta, hit.point.y + _building.GetSize().y / 2, hit.point.z - hit.point.z % _fieldDelta);
        }

        //Rotate
        if (Input.GetKeyDown(KeyCode.R))
        {
            _buildingGO.transform.Rotate(Vector3.up, 90);
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            _buildingGO.transform.Rotate(Vector3.up, -90);
        }

        if(Input.GetKeyDown(KeyCode.Q) && _floor.IsItPossibleToBuild(_building)) {
            _building.SetUsedPoints( _floor.TryToBuild(_building));

            _handler.AddBuilding((_building, _buildingGO));
            _building = null;

            _buildingGO.GetComponentInChildren<BoxCollider>().enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            GameObject.Destroy(_buildingGO);
            _handler.MoveToNext();
        }

        if(Input.GetKeyDown(KeyCode.Escape)) { 
            GameObject.Destroy(_buildingGO);
            _handler.Declain();
        }
    }
}
