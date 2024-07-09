
using UnityEngine;

public class ZombieHealthController : MonoBehaviour, IDestroyable
{
    [SerializeField] private double _health;
    
    public void AddHealth(double health)
    {
        throw new System.NotImplementedException();
    }

    public bool IsAlive()
    {
        return _health > 0;
    }

    public void RemoveHealth(double health)
    {
        _health -= health;
    }
}
