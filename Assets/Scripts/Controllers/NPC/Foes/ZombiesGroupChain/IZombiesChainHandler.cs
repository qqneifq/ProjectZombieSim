using System.Collections.Generic;
using UnityEngine;

public interface IZombiesChainHandler
{
    public void MoveToNext();

    public HashSet<ZombieMainController> GetZombies();

    public void AddZombie(ZombieMainController zombie);

    public void RemoveZombie(ZombieMainController zombie);
}
