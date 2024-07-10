using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiesCreating : IChainPart
{
    private IZombiesChainHandler _handler;

    private Transform _mainTarget;

    private GameObject _zombiePrefab;


    private Vector3 _startPoint;
    private Vector3 _size;

    private Vector3 _zombieSize;

    private int _count;


    public ZombiesCreating(IZombiesChainHandler handler, Transform mainTarget, GameObject zombiePrefab, Vector3 startPoint, Vector3 size, Vector3 zombieSize, int count)
    {
        _handler = handler;
        _mainTarget = mainTarget;
        _zombiePrefab = zombiePrefab;
        _startPoint = startPoint;
        _size = size;
        _zombieSize = zombieSize;
        _count = count;
    }

    public void Update(double delta)
    {
        int maxInLength = (int)(_size.z / (_zombieSize.z + 0.4f));
        int maxInWidth = (int)(_size.x / (_zombieSize.x + 0.4f));

        Vector3 z = new Vector3 (0,0,_zombieSize.z + 0.4f);
        Vector3 x = new Vector3(_zombieSize.x + 0.4f, 0, 0);
        for (int i = 0; i < _count/maxInLength; i++)
        {
            for(int j = 0; j < maxInWidth; j++)
            {
                GameObject zombie = GameObject.Instantiate(_zombiePrefab, _startPoint + z * j + x * i, _zombiePrefab.transform.rotation);

                ZombieHabbitHandler zombieHabbitHandler = new ZombieHabbitHandler(zombie, _mainTarget);
                ZombieHealthController zombieHealthController = zombie.GetComponent<ZombieHealthController>();

                _handler.AddZombie(new ZombieMainController(_handler, zombieHealthController, zombieHabbitHandler, zombie));
            }
        }

        for(int i = 0; i < _count % maxInLength; i++)
        {
            GameObject zombie = GameObject.Instantiate(_zombiePrefab, _startPoint + maxInWidth * x + z * i, _zombiePrefab.transform.rotation);

            ZombieHabbitHandler zombieHabbitHandler = new ZombieHabbitHandler(zombie, _mainTarget);
            ZombieHealthController zombieHealthController = zombie.GetComponent<ZombieHealthController>();

            _handler.AddZombie(new ZombieMainController(_handler, zombieHealthController, zombieHabbitHandler, zombie));
        }
        _handler.MoveToNext();
    }
}
