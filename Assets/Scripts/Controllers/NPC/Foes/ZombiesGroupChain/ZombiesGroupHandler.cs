using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ZombiesGroupHandler : MonoBehaviour, IZombiesChainHandler
{
    [SerializeField] private GameObject _zombiePrefab;
    [SerializeField] private Transform _pivot;

    [SerializeField] private Vector3 _spawnSize;
    [SerializeField] private Vector3 _zombieSize;
    [SerializeField] private float _zombieCreatingCoolDown;
    [SerializeField] private int _count;

    private HashSet<ZombieMainController> _zombies;

    private List<IChainPart> _parts;
    private int _index;

    void Start()
    {
        _zombies = new HashSet<ZombieMainController>();
        _parts = new List<IChainPart>();

        ZombiesWait wait = new ZombiesWait(this, _zombieCreatingCoolDown);
        ZombiesCreating creating = new ZombiesCreating(this, GameObject.FindAnyObjectByType<BaseHealthController>().transform, _zombiePrefab,_pivot.position, _spawnSize, _zombieSize, _count);
        ZombiesManage manage = new ZombiesManage(this);

        _parts.Add(wait);
        _parts.Add(creating);
        _parts.Add(manage);

        _index = 0;
    }
    public void AddZombie(ZombieMainController zombie)
    {
        _zombies.Add(zombie);
    }

    public HashSet<ZombieMainController> GetZombies()
    {
        return _zombies;
    }

    public void MoveToNext()
    {
        _index = (_index + 1) % _parts.Count;
    }

    public void RemoveZombie(ZombieMainController zombie)
    {
        _zombies.Remove(zombie);
    }

    // Update is called once per frame
    void Update()
    {
        _parts[_index].Update(Time.deltaTime);
    }
}
