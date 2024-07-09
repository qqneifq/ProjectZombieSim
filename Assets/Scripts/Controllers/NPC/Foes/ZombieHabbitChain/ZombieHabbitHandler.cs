using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHabbitHandler : IZombieChainHandler, IChainPart
{
    private List<IChainPart> _chains;

    private Transform _body;
    private Transform _target;

    private Animator _animator;

    private Floor _floor;

    private float _speed, _attackStrength, _attackCoolDown, _sphereRadiusCheck, _maxDistanceReaction;

    private int _index = 0;

    public ZombieHabbitHandler(GameObject body, Transform mainTarget)
    {
        _floor = GameObject.FindFirstObjectByType<Floor>();

        _body = body.transform;
        _target = mainTarget;

        _animator = body.GetComponent<Animator>();

        ZombieData data = body.GetComponent<ZombieData>();

        _speed = data.GetSpeed();
        _attackStrength = data.GetAttackStrength();
        _attackCoolDown = data.GetAttackCoolDown();
        _sphereRadiusCheck = data.GetSphereRadius();
        _maxDistanceReaction = data.GetDistanceTrigger();
        _chains = new List<IChainPart>();

        chainCreate();
    }

    private void chainCreate()
    {
        ZombieTargetLooker targetLooker = new ZombieTargetLooker(this, _target, _body, _floor, (int)_maxDistanceReaction, _floor.GetDelta());
        ZombieMove zombieMove = new ZombieMove(this, _body, 0.2f, 0.4f, _speed);
        ZombieAttack zombieAttack = new ZombieAttack(this, _attackCoolDown, _attackStrength);

        _chains.Add(targetLooker);
        _chains.Add(zombieMove);
        _chains.Add(zombieAttack);
    }

    public GameObject GetTarget()
    {
        if( _target == null )
        {
            return null;
        }
        return _target.gameObject;
    }

    public void MoveToNext()
    {
        _index = (_index + 1) % _chains.Count;
    }

    public void SetAnimationAttackSpeed(float speed)
    {
        throw new System.NotImplementedException();
    }

    public void SetAnimtionWalkSpeed(float speed)
    {
        throw new System.NotImplementedException();
    }

    public void SetTarget(GameObject target)
    {
        _target = target.transform;
    }

    public void UpdateTarget()
    {
        _index = 0;
    }

    public void Update(double delta)
    {
        if(_index == 0)
        {
            Debug.Log("Some shit happens");
        }
        _chains[_index].Update(delta);
    }
}
