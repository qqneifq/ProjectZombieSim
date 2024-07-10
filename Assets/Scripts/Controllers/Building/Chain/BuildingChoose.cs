using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static BuildingsConsts;

public class BuildingChoose : IChainPart
{
    private IBuildingChainHandler _handler;

    private Transform _face;
    private LayerMask _groundMask;

    private BuildingsFactory _buildingsFactory;

    private BuildingContoller _building;
    private GameObject _buildingGO = null;

    private float _raycastField;
    private float _fieldDelta;
    private float _wheel;
    public BuildingChoose(IBuildingChainHandler handler, Transform face, LayerMask ground, BuildingsFactory buildingsFactory, float raycastField, float fieldDelta)
    {
        _handler = handler;
        _face = face;

        _groundMask = ground;   
        _buildingsFactory = buildingsFactory; 
        
        _raycastField = raycastField;   
        _fieldDelta = fieldDelta;
    }

    public void Update(double delta)
    {        
        if (_building == null)
        {
            Building building = _buildingsFactory.Get(0);
            _buildingGO = GameObject.Instantiate(building._gameBody);
            _building = _buildingGO.GetComponent<BuildingContoller>();
            _building.SetData(building);

            _buildingGO.GetComponentInChildren<BoxCollider>().enabled = false;
        }

        //init
        RaycastHit hit;
        Vector3 fwd = _face.forward;

        Debug.DrawRay(_face.position + fwd * 1, fwd, Color.blue);

        int _buildingIndificator = (int)_building.GetIndificator();

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

        //Change
        _wheel += Input.GetAxis("Mouse ScrollWheel");
        if (_wheel * _wheel > 0.001f)
        {
            Debug.Log("NOT ZERO: " + _wheel);
        }
        Building building1;
        if (_wheel < -1f)
        {
            _wheel = 0;
            Debug.Log("Next building");

            GameObject.Destroy(_buildingGO);

            if (_buildingIndificator + 1 >= Enum.GetValues(typeof(BuildingIndificator)).Length)
            {
                _buildingIndificator = 0;
            }
            else
            {
                _buildingIndificator += 1;
            }

            building1 = _buildingsFactory.Get((BuildingIndificator)_buildingIndificator);

            while (building1 == null)
            {
                if (_buildingIndificator + 1 >= Enum.GetValues(typeof(BuildingIndificator)).Length)
                {
                    _buildingIndificator = 0;
                }
                else
                {
                    _buildingIndificator += 1;
                }
                building1 = _buildingsFactory.Get((BuildingIndificator)_buildingIndificator);
            }

            _buildingGO = GameObject.Instantiate(building1._gameBody);
            _building = _buildingGO.GetComponent<BuildingContoller>();
            _building.SetData(building1);
        }
        else if (_wheel > 1f)
        {
            _wheel = 0;
            Debug.Log("Previous building");

            GameObject.Destroy(_buildingGO);

            if (_buildingIndificator - 1 < 0)
            {
                _buildingIndificator = (Enum.GetValues(typeof(BuildingIndificator)).Length - 1);
            }
            else
            {
                _buildingIndificator--;
            }

            building1 = _buildingsFactory.Get((BuildingIndificator)_buildingIndificator);

            while (building1 == null)
            {
                if (_buildingIndificator - 1 < 0)
                {
                    _buildingIndificator = (Enum.GetValues(typeof(BuildingIndificator)).Length - 1);
                }
                else
                {
                    _buildingIndificator--;
                }

                building1 = _buildingsFactory.Get((BuildingIndificator)_buildingIndificator);
            }

            _buildingGO = GameObject.Instantiate(building1._gameBody);
            _building = _buildingGO.GetComponent<BuildingContoller>();
            _building.SetData(building1);
        }

        //Player want to build a building
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject.Destroy(_buildingGO);

            _handler.SetBuilding((int)_building.GetIndificator());
            _handler.MoveToNext();
        }

        //On declain action
        if(Input.GetKeyDown(KeyCode.Escape)) {
            GameObject.Destroy(_buildingGO);

            _handler.Declain();
        }
    }
}
