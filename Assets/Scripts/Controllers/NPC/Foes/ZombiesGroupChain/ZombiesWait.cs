using Unity.VisualScripting;

public class ZombiesWait : IChainPart
{
    private IZombiesChainHandler _handler;

    private float _coolDown;
    private float _time;

    public ZombiesWait(IZombiesChainHandler handler, float coolDown)
    {
        _handler = handler;
        _coolDown = coolDown;
        _time = coolDown;
    }

    public void Update(double delta)
    {
        _time -= (float) delta;

        if (_time < 0)
        {
            _time = _coolDown;

            _handler.MoveToNext();
        }
    }
}
