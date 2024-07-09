using System;

public class ResourceTypes
{
    public enum ResourcesEnum
    {
        Stone = 0,
        Wood = 1,
        People = 2,
        Food = 3,
        Energy = 4
    }

    public static int Count()
    {
        return Enum.GetNames(typeof(ResourcesEnum)).Length;
    }

    public static string Name(int index)
    {
        return Enum.GetName(typeof(ResourcesEnum), index);
    }
    public static int Index(ResourcesEnum e)
    {
        return (int)e;
    }
    public static ResourcesEnum Type(int i)
    {
        if(i <= Count() - 1)
        {
            return (ResourcesEnum)i;
        }
        else
        {
            throw new Exception($"??? ?????? ??????? ({i})");
        }
    }
}
