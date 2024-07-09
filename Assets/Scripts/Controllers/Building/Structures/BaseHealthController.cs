using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealthController : MonoBehaviour, IDestroyable
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _health;
    
    public void AddHealth(double health)
    {
        _health = Mathf.Max(_health + (float) health, _maxHealth);
    }

    public bool IsAlive()
    {
        return _health > 0;
    }

    public void RemoveHealth(double health)
    {
        _health -= (float) health;
    }

    void Start()
    {
        _health = _maxHealth;
    }

    void Update()
    {
        if(!IsAlive())
        {
            Debug.Log("END GAME");
        }
    }
}
