using System;

public class ResourceTypes
{
    public enum ResourcesEnum
    {
        Stone, Wood, People, Food, Energy
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
