using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTypes
{
    public enum ResourcesEnum
    {
        Stone, Wood, People, Food
    }

    public static int Count()
    {
        return Enum.GetNames(typeof(ResourcesEnum)).Length;
    }

    public static string Name(int index)
    {
        return Enum.GetName(typeof(ResourcesEnum), index);
    }
}
