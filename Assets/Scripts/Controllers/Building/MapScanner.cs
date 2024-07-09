using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScanner : MonoBehaviour
{
    private Floor _floor;

    void Start()
    {
        /*_floor = FindAnyObjectByType<Floor>();

        MapBuilding[] list = FindObjectsOfType<MapBuilding>();

        foreach(var obj in list)
        {
            _floor.TryToBuild((obj.getPivot().position, obj.getPivot().rotation, gameObject), obj.getSize());
        }*/
    }

}
