using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static BuildingsConsts;

public class PlayerBuilder : MonoBehaviour
{
    [SerializeField] private Transform _face;
    [SerializeField] private LayerMask _groundMask;

    [SerializeField] private BuildingsFactory _buildingsFactory;
    [SerializeField] private float _raycastField;
    [SerializeField] private float _rotationSpeed;

    private Floor _floorController;
    private GameObject _buildingGO = null;

    private Building _building;
    private bool _isBuilding;
    private float _wheel;
    private BuildingIndificator _buildingIndificator;

    private static event Action<String> onBuildingIdChange;
    private static event Action<bool> onBuildingRequest;
    public static event Action<bool> OnBuildingRequest
    {
        add
        {
            onBuildingRequest += value;
        }
        remove
        {
            onBuildingRequest -= value;
        }
    }
    public static event Action<String> OnBuildingIdChange
    {
        add
        {
            onBuildingIdChange += value;
        }
        remove
        {
            onBuildingIdChange -= value;
        }
    }


    private void Start()
    {
        _wheel = 0;
        _floorController = FindObjectOfType<Floor>();
    }
    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = _face.forward;

        Debug.DrawRay(_face.position + fwd * 1, fwd, Color.blue);
        //Move
        if(_buildingGO != null && Physics.Raycast(_face.position, fwd, out hit, _raycastField, _groundMask))
        {
            _buildingGO.transform.position = new Vector3(hit.point.x - hit.point.x % _floorController.getDelta(), hit.point.y + _building._size.y/2, hit.point.z - hit.point.z % _floorController.getDelta());
        }

        //rotating
        if (_buildingGO != null )
        {
           if (Input.GetKeyDown(KeyCode.R))
           {
                _buildingGO.transform.Rotate(Vector3.up, 45);
           } else if (Input.GetKeyDown(KeyCode.T))
           {
                _buildingGO.transform.Rotate(Vector3.up, -45);

           }
        }
        BuildingContoller buildingContoller = null;
        if (_building != null)
        {
            buildingContoller = _buildingGO.GetComponent<BuildingContoller>();
            buildingContoller.SetData(_building);
        }

        //Open\Close\Create\Destroy
        if (Input.GetKeyDown(KeyCode.Z) && Physics.Raycast(_face.position, fwd, out hit, _raycastField) && (hit.transform.CompareTag("Building") || hit.transform.CompareTag("Storage"))) {

            BuildingContoller buildingContollerDelete = hit.transform.GetComponent<BuildingContoller>();
            _floorController.ReleaseBuildingPoints(buildingContollerDelete.getUsedPoints());
            buildingContollerDelete.SetUsedPoints(null);


            Destroy(hit.transform.gameObject);
        }
        else if (_building == null && Input.GetKeyDown(KeyCode.B) && !_isBuilding && Physics.Raycast(_face.position, fwd, out hit, _raycastField, _groundMask)) {
             _floorController.CreateBuildingMap();
            _building = _buildingsFactory.Get(0);
            //onBuildingIdChange?.Invoke(_building.getName());
            _buildingGO = Instantiate(_building._gameBody, transform.position, transform.rotation);
            _buildingGO.GetComponentInChildren<BoxCollider>().isTrigger = true;

            _buildingIndificator = 0;
            _isBuilding = true;

            //onBuildingRequest?.Invoke(true);
        }
        else if (_buildingGO != null && Input.GetKeyDown(KeyCode.Q)
            && _floorController.IsItPossibleToBuild(buildingContoller)){
            _buildingGO.GetComponentInChildren<BoxCollider>().isTrigger = false;
            _floorController.TryToBuild(buildingContoller);
            _floorController.DestroyBuildingMap();

            _building = null;
            _buildingGO = null;
            _isBuilding = false;
            //onBuildingRequest?.Invoke(false);

        }
        else if (_buildingGO != null && Input.GetKeyDown(KeyCode.Escape)) {
            _floorController.DestroyBuildingMap();

            _isBuilding = false;
            Destroy(_buildingGO);

            _building = null;
        }
    }
}
