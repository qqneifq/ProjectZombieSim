using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieData : MonoBehaviour
{
    [SerializeField] private float _attackCoolDown;
    [SerializeField] private float _attackStrength;
    [SerializeField] private float _speed;
    [SerializeField] private float _sphereRadiusCheck;
    [SerializeField] private float _distanceTrigger;

    public float GetSpeed() { return _speed; }
    public float GetAttackCoolDown() { return _attackCoolDown; }
    public float GetAttackStrength() { return _attackStrength; }
    public float GetSphereRadius() { return _sphereRadiusCheck; }
    public float GetDistanceTrigger() { return _distanceTrigger; }  
}
