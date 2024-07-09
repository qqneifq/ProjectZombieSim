using UnityEngine;

public class ZombieMainController : IChainPart
{
    private IZombiesChainHandler _handler;    
    private ZombieHabbitHandler _zombieHabbitController;
    private IDestroyable _zombieHealthController;

    private GameObject _zombie;

    public ZombieMainController(IZombiesChainHandler handler, IDestroyable zombieHealthController, ZombieHabbitHandler zombieHabbitHandler, GameObject zombie)
    {
        _handler = handler;
        _zombieHabbitController = zombieHabbitHandler;
        _zombieHealthController = zombieHealthController;   
        _zombie = zombie;
    }

    public void Update(double delta)
    {
        if (_zombieHealthController.IsAlive())
        {
            _zombieHabbitController.Update(delta);
        } else
        {
            _handler.RemoveZombie(this);

            GameObject.Destroy(_zombie);
        }
    }
}
