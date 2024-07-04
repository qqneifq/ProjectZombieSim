using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilding : MonoBehaviour
{
    [SerializeField] private Vector3 size;
    [SerializeField] private Transform pivot;

    public Vector3 getSize()
    {
        return size;
    }

    public Transform getPivot()
    {
        return pivot;
    }
}
