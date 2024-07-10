using System;
using Unity.VisualScripting;

public class ZombiesWait : IChainPart
{
    private IZombiesChainHandler _handler;

    private float _coolDown;
    private float _time;
    private float _elapsed = 1f;
    public static event Action<float> OnTimerTick;

    public ZombiesWait(IZombiesChainHandler handler, float coolDown)
    {
        _handler = handler;
        _coolDown = coolDown;
        _time = coolDown;
        OnTimerTick?.Invoke(_elapsed);
    }

    public void Update(double delta)
    {
        _time -= (float) delta;
        _elapsed += (float) delta;

        if(_elapsed >= 1f)
        {
            OnTimerTick?.Invoke(_time);
            _elapsed = 0f;
        }

        if (_time < 0)
        {
            _time = _coolDown;

            _handler.MoveToNext();
        }
    }
    
}
