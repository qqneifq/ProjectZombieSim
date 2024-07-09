using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    float health;
    public static event Action OnDeath;
    public Health(float health)    {
        this.health = health;
    }
    void ChangeHealth(float h)
    {

    }
}
