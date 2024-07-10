using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// put on attackable world objects
public class HealthModel : MonoBehaviour, IDestroyable
{
    [SerializeField]
    float health;
    [SerializeField]
    float armor;
    public static event Action<GameObject> OnDeath;
    public HealthModel(float health, float armor)
    {
        this.health = health;
        this.armor = armor;
    }
    public void RemoveHealth(double h)
    {
        if(h > armor)
        {
            health -= (float)h - armor;
            
        }
        else
        {
            health--;
        }
        if (health <= 0)
        {
            OnDeath?.Invoke(gameObject);
        }
    }

    public void AddHealth(double health)
    {
        this.health += (float)health;
    }

    public bool IsAlive()
    {
        return health > 0;
    }
}
