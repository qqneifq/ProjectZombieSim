using System.Collections;
using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;

public class FencePostProcessor : IBuildingPostprocessor
{
    private Floor _floor;
    private BuildingsFactory _buildingFactory;
    public FencePostProcessor(Floor floor, BuildingsFactory buildingFactory)
    {
        _floor = floor;
        _buildingFactory = buildingFactory;
    }

    public List<GameObject> Process(List<GameObject> initialStructures)
    {
        if(initialStructures == null || initialStructures.Count <= 1)
        {
            return initialStructures;
        }

        List<GameObject> gameObjects = new List<GameObject>();

        Building data = _buildingFactory.Get(BuildingsConsts.BuildingIndificator.Wall);

        GameObject prefab = data._gameBody;
        Vector3 size = data._size;
        Quaternion quaternion = initialStructures[0].transform.rotation;
        
        gameObjects.Add(initialStructures[0]);
        for (int i = 1; i < initialStructures.Count; i++)
        {
            Vector3 from = initialStructures[i - 1].transform.position;
            Vector3 to = initialStructures[i].transform.position;


            (float fromX, float toX) px = (Mathf.Min(from.x, to.x), Mathf.Max(from.x, to.x));
            (float fromZ, float toZ) pz = (Mathf.Min(from.z, to.z), Mathf.Max(from.z, to.z));

            for(float x = px.fromX + _floor.GetDelta(); x <= px.toX; x += _floor.GetDelta())
            {
                float z = fz(x, to.x, from.x, to.z, from.z);

                Vector3 newObj = new Vector3(x, to.y, z);

                if (_floor.IsItPossibleToBuild((newObj, quaternion), size)) {
                    GameObject instance = GameObject.Instantiate(prefab, newObj, quaternion);
                    instance.GetComponent<BuildingContoller>().SetData(data);

                    _floor.TryToBuild(instance.GetComponent<BuildingContoller>());
                    gameObjects.Add(instance);
                }
            }

            for (float z = pz.fromZ + _floor.GetDelta(); z <= pz.toZ; z += _floor.GetDelta())
            {
                float x = fx(z, to.x, from.x, to.z, from.z);

                Vector3 newObj = new Vector3(x, to.y, z);

                if (_floor.IsItPossibleToBuild((newObj, quaternion), size))
                {
                    GameObject instance = GameObject.Instantiate(prefab, newObj, quaternion);
                    instance.GetComponent<BuildingContoller>().SetData(data);

                    _floor.TryToBuild(instance.GetComponent<BuildingContoller>());
                    gameObjects.Add(instance);
                }
            }
            gameObjects.Add(initialStructures[i]);
        }

        return gameObjects;
    }

    private float fx(float z, float x0, float x1, float z0, float z1)
    {
        float xd = x1 - x0;
        float zd = z1 - z0;

        return (z - z0) / zd * xd + x0;
    }

    private float fz(float x, float x0, float x1, float z0, float z1)
    {
        float xd = x1 - x0;
        float zd = z1 - z0;

        return (x - x0) / xd * zd + z0;
    }
}
