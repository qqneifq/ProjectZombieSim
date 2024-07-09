using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHealthController : MonoBehaviour, IDestroyable
{
    [SerializeField] private float _initialHealth = 10;
    [SerializeField] private float _health;
    private bool _setToDestroy = false;

    private BuildingContoller _buildingContoller;
    private Floor _floor;

    public void Start()
    {
        _health = _initialHealth;
        _buildingContoller = this.GetComponent<BuildingContoller>();
        _floor = FindAnyObjectByType<Floor>();
    }

    public void AddHealth(double health)
    {
        _health = Mathf.Min(_health + (float)health, _initialHealth);
    }

    public bool IsAlive()
    {
        return _health > 0;
    }

    public void RemoveHealth(double health)
    {
        _health -= (float)health;
    }


    // Update is called once per frame
    void Update()
    {
        if(_health < 0.01 && !_setToDestroy) {
            _setToDestroy = true;
        }

        if(_setToDestroy)
        {
            if(_buildingContoller.getUsedPoints() != null && _buildingContoller.getUsedPoints().Count > 0)
            {
                _floor.ReleaseBuildingPoints(_buildingContoller);
            }
            Destroy(gameObject);
        }
    }
}
