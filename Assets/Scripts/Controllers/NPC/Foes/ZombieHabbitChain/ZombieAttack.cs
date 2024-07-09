using UnityEngine;

public class ZombieAttack : IChainPart
{
    private IZombieChainHandler _handler;
    private IDestroyable _target;
    private float _attackRecharge;
    private float _attackStrength;

    private float _attackCoolDown;
    public ZombieAttack(IZombieChainHandler handler, float attackTime, float attackStrength)
    {
        _handler = handler;

        _attackRecharge = attackTime;
        _attackStrength = attackStrength;

        _attackCoolDown = 0;
    }

    public void Update(double delta)
    {
        if (_handler.GetTarget() == null)
        {
            _handler.MoveToNext();
            return;
        }
        _target = _handler.GetTarget().GetComponent<IDestroyable>();

        if(_target.IsAlive())
        {
            if (_attackCoolDown <= 0)
            {
                _target.RemoveHealth(_attackStrength);
                _attackCoolDown = _attackRecharge;
            }
            _attackCoolDown -= (float) delta;
        }
    }
}
