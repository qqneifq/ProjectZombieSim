using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModel : MonoBehaviour
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
    public void RemoveHealth(float h)
    {
        if(h > armor)
        {
            health -= h;
            if(health < 0)
            {
                OnDeath?.Invoke(gameObject);
            }
        }
    }
}
