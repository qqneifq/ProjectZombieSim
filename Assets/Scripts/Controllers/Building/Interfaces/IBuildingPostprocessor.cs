using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuildingPostprocessor
{
    public List<GameObject> Process(List<GameObject> initialStructures);
}
