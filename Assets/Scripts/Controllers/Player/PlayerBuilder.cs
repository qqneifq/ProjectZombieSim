using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static BuildingsConsts;

public class PlayerBuilder : MonoBehaviour
{


    private GameObject _buildingGO = null;

    private BuildingIndificator _buildingIndificator;
    private Building _building;
    private bool _isBuilding;

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

    }
    void Update()
    {
        

        //Open\Close\Create\Destroy
        if (Input.GetKeyDown(KeyCode.Z) && Physics.Raycast(_face.position, fwd, out hit, _raycastField) && (hit.transform.CompareTag("Building") || hit.transform.CompareTag("Storage"))) {

            BuildingContoller buildingContollerDelete = hit.transform.GetComponent<BuildingContoller>();
            _floorController.ReleaseBuildingPoints(buildingContollerDelete.getUsedPoints());
            buildingContollerDelete.SetUsedPoints(null);


            Destroy(hit.transform.gameObject);
        }
        else 
        else if (_buildingGO != null && Input.GetKeyDown(KeyCode.Q)
            && _floorController.IsItPossibleToBuild(buildingContoller)
            && _resourceHandler.IsEnoughResources(_building._buildingConditions))
        {
            _buildingGO.GetComponentInChildren<BoxCollider>().isTrigger = false;
            _floorController.TryToBuild(buildingContoller);
            _floorController.DestroyBuildingMap();
            _resourceHandler.RemoveResources(_building._buildingConditions);

            _building = null;
            _buildingGO = null;
            _isBuilding = false;
            onBuildingRequest?.Invoke(false);

        }
        else if (_buildingGO != null && Input.GetKeyDown(KeyCode.Escape)) {
            _floorController.DestroyBuildingMap();

            _isBuilding = false;
            Destroy(_buildingGO);

            _building = null;
        }
    }

    public void ChangeGameObject(GameObject newGO)
    {

    }
}
