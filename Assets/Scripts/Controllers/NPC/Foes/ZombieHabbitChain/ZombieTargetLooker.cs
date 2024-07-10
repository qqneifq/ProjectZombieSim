using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieTargetLooker : IChainPart
{
    private IZombieChainHandler _handler;
    private Transform _mainTarget;
    private Transform _body;
    private Floor _floor;

    private int _radius;
    private float _delta;

    public ZombieTargetLooker(IZombieChainHandler handler, Transform mainTarget, Transform body, Floor floor, int radius, float floorDelta)
    {
        _handler = handler;
        _mainTarget = mainTarget;
        _body = body;
        _floor = floor;
        _radius = radius;
        _delta = floorDelta;

        _handler.SetTarget(mainTarget.gameObject);
    }

    public void Update(double delta)
    {
        //Check for Player or Soldiers near
        RaycastHit[] gameObjects = Physics.SphereCastAll(_body.position, _radius, _body.forward, 0.1f);
        foreach (RaycastHit gameObject in gameObjects)
        {
            if(gameObject.transform.tag != "Foe" && gameObject.transform.tag != "Enviroment" && gameObject.transform.GetComponent<IDestroyable>() != null)
            {
                _handler.SetTarget(gameObject.transform.gameObject);
                _handler.MoveToNext();
                return;
            }
        }
        //Check for Buildings near
        Vector3 directionBuf = new Vector3();   

        for(int i = -_radius; i<= _radius; i++)
        {
            for(int j = -_radius; j<= _radius; j++)
            {
                directionBuf.Set(i*_delta, 0, j*_delta);

                GameObject toCheck = _floor.GetGameObjectByPoint(directionBuf + _body.position);
                if (toCheck != null && toCheck.GetComponent<IDestroyable>() != null)
                {
                    _handler.SetTarget(toCheck);
                    _handler.MoveToNext();
                    return;
                }
            }
        }

        _handler.SetTarget(_mainTarget.gameObject);
        _handler.MoveToNext();
        return;
    }
}
