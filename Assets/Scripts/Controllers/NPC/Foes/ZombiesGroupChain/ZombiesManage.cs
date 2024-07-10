public class ZombiesManage : IChainPart
{
    IZombiesChainHandler _handler;

    public ZombiesManage(IZombiesChainHandler handler)
    {
        _handler = handler;
    } 

    public void Update(double delta)
    {
        if (_handler.GetZombies().Count > 0)
        {
            foreach(var z in _handler.GetZombies())
            {
                z.Update(delta);
            }
        } else
        {
            _handler.MoveToNext();
        }
    }
}
