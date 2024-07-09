using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] private GameObject _greenDebugPlane;
    [SerializeField] private GameObject _redDebugPlane;

    [SerializeField] private Transform _pointStart;
    [SerializeField] private Vector3 _size;

    [SerializeField] private float _matrixDivisionUnit = 0.5f;

    private FloorController _buildingMatrixController;
    private PathFindingController _pathFindingController;
    private DebugBuildingLayout _debugBuildingLayout;

    private Dictionary<(int x, int z), GameObject> _savedObjects;

    //OZ axis (local)
    private Vector3 _correctForwardVector;
    //OX axis (local)
    private Vector3 _correctRightVector;

    //First value by OX, second value by OZ axis
    private (int, int) _realMatrixSize;
    void Start()
    {
        _savedObjects = new Dictionary<(int x, int z), GameObject>();

        _correctForwardVector = _pointStart.forward * _matrixDivisionUnit;
        _correctRightVector = _pointStart.right * _matrixDivisionUnit;
        
        _realMatrixSize = ((int)(_size.x / _matrixDivisionUnit), (int)(_size.z / _matrixDivisionUnit));

        _buildingMatrixController = new FloorController(_realMatrixSize, _pointStart, _correctForwardVector, _correctRightVector, _matrixDivisionUnit);
        _pathFindingController = new PathFindingController(_realMatrixSize, _correctForwardVector, _correctRightVector, _pointStart.position);
        _debugBuildingLayout = new DebugBuildingLayout(_greenDebugPlane, _redDebugPlane, FindAnyObjectByType<PlayerMovement>().transform, _pointStart, _realMatrixSize, _matrixDivisionUnit, _correctForwardVector, _correctRightVector);
    }

    public void Update()
    {
        _debugBuildingLayout.update();
    }

    //BuildingMatrixController methods
    public bool IsItPossibleToBuild(BuildingContoller buildingContoller)
    {
        return _buildingMatrixController.IsItPossibleToBuild(buildingContoller);
    }
    public bool IsItPossibleToBuild((Vector3, Quaternion) transform, Vector3 size)
    {
        return _buildingMatrixController.IsItPossibleToBuild(transform, size);
    }
    public List<(int, int)> TryToBuild(BuildingContoller buildingContoller)
    {
        List<(int, int)> ans = _buildingMatrixController.TryToBuild(buildingContoller);
        buildingContoller.SetUsedPoints(ans);

        savePointsWithObject(ans, buildingContoller.gameObject);
        return ans;
    }
    
    public List<(int, int)> TryToBuild((Vector3 position, Quaternion rotation, GameObject gameObject)  buildingContoller, Vector3 size)
    {
        List<(int, int)> ans = _buildingMatrixController.TryToBuild((buildingContoller.position, buildingContoller.rotation), size);

        savePointsWithObject(ans, buildingContoller.gameObject);
        return ans;
    }

    public void ReleaseBuildingPoints(List<(int, int)> points)
    {
        if (points == null) return;
        deletePointsWithObject(points);
        _buildingMatrixController.ReleasePoints(points);
    }

    public GameObject GetGameObjectByPoint(Vector3 point)
    {
        (int x, int z) analyze = ((int)((point.x - _pointStart.position.x) / _matrixDivisionUnit), (int)((point.z - _pointStart.position.z) / _matrixDivisionUnit));
        GameObject ans;
        if (_savedObjects.TryGetValue(analyze, out ans))
        {
            return ans;
        }
        else
        {
            return null;
        }
    }
    public bool IsPointUsed(Vector3 point)
    {
        (int x, int z) analyze = ((int)((point.x - _pointStart.position.x) / _matrixDivisionUnit), (int)((point.z - _pointStart.position.z) / _matrixDivisionUnit));
        
        return _savedObjects.ContainsKey(analyze);
    }
    //DebugBuildingLayout methods
    public void CreateBuildingMap() {
        _debugBuildingLayout.CreateBuildingMap(_buildingMatrixController.getBuildingMatrix());
    }

    public void DestroyBuildingMap()
    {
        _debugBuildingLayout.DestroyBuildingMap();
    }

    public float GetDelta()
    {
        return _matrixDivisionUnit;
    }

    private void savePointsWithObject(List<(int, int)> points, GameObject gameObject) {
        foreach (var pair in points)
        {
            _savedObjects.Add(pair, gameObject);
        }
    }
    private void deletePointsWithObject(List<(int, int)> points)
    {
        foreach (var pair in points)
        {
            _savedObjects.Remove(pair);
        }
    }

    
}
