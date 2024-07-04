using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public class DebugBuildingLayout : MonoBehaviour
{
    private GameObject _greenDebugPlane;
    private GameObject _redDebugPlane;

    private Transform _player;

    private Transform _pointStart;
    private (int, int) _size;
    private float _delta;
    private Dictionary<(int, int), GameObject> _stack;
    //OZ axis (local)
    private Vector3 _correctForwardVector;
    //OX axis (local)
    private Vector3 _correctRightVector;

    private int _radius = 10;

    private bool _on = false;
    private bool[,] _buildingMatrix;
    public DebugBuildingLayout(GameObject greenDebugPlane, GameObject redDebugPlane, Transform player, Transform pointStart, (int, int) size, float delta, Vector3 correctForwardVector, Vector3 correctRightVector)
    {
        _greenDebugPlane = greenDebugPlane;
        _redDebugPlane = redDebugPlane;
        _delta = delta;
        _player = player;   

        _pointStart = pointStart;
        _size = size;
            
        _correctForwardVector = correctForwardVector;
        _correctRightVector = correctRightVector;
        _stack = new Dictionary<(int, int), GameObject>();
    }

    public void update()
    {
        if (_on)
        {
            for(float i = (int)(_player.position.x - _radius); i <= (int)(_player.position.x + _radius); i+=_delta) { 
                for(float j = (int)(_player.position.z - _radius); j <= (int)(_player.position.z + _radius); j+=_delta)
                {
                    (int x, int y) newPoint = ((int)((i - _pointStart.position.x) / _delta), (int)((j - _pointStart.position.z) / _delta));
                    if (newPoint.Item1 >= 0 && newPoint.Item1 < _size.Item1 && newPoint.Item2 >= 0 && newPoint.Item2 < _size.Item2)
                    {
                        if (!_buildingMatrix[newPoint.x, newPoint.y])
                        {
                            buildObstacle(_greenDebugPlane, newPoint.x, newPoint.y, 1);
                        }
                        else
                        {
                            buildObstacle(_redDebugPlane, newPoint.x, newPoint.y, 1.1f);
                        }
                    }
                }
            }
            Stack<(int, int)> onDestroy = new Stack<(int, int)> ();
            foreach(var i in _stack)
            {
                Vector3 newPos = (i.Key.Item2) * _correctForwardVector + (i.Key.Item1) * _correctRightVector + _pointStart.position;
                if ((newPos - _player.position).sqrMagnitude > _radius*_radius)
                {
                    onDestroy.Push(i.Key);
                    Destroy(i.Value);
                }
            }
            while(onDestroy.Count > 0)
            {
                _stack.Remove(onDestroy.Pop());
            }
        }
    }
 
    
    //Debug priority purpose
    public void CreateBuildingMap(bool[,] buildingMatrix)
    {
        _on = true;
        _buildingMatrix = buildingMatrix;

    }

    public void DestroyBuildingMap()
    {
        _on = false;
        foreach(var i in _stack)
        {
            Destroy(i.Value);
        }
        _stack.Clear();
    }
    private void buildObstacle(GameObject gameObject, int i, int j, float yd)
    {
        if(_stack.ContainsKey((i, j))) {
            return;
        }

        Vector3 x = (j) * _correctForwardVector;
        Vector3 z = (i) * _correctRightVector;
        Vector3 y = _pointStart.up * yd * 0.01f;
        
        _stack.Add((i, j), Instantiate(gameObject, x + z + y + _pointStart.position, _pointStart.rotation));
    }
}
