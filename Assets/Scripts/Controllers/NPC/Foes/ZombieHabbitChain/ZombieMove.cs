using UnityEngine;

public class ZombieMove : IChainPart
{
    private IZombieChainHandler _handler;

    private Transform _body;
    private Transform _target;

    private float _raycastDistance;
    private float _raycastSphereRadius;

    private float _speed;
    public ZombieMove(IZombieChainHandler handler, Transform body, float raycastSphereRadius, float raycastDistance, float speed)
    {
        _handler = handler;
        _body = body;
        _raycastDistance = raycastDistance;
        _raycastSphereRadius = raycastSphereRadius;
        _speed = speed;
    }

    public void Update(double delta)
    {
        if(_handler.GetTarget() == null) {
            _handler.UpdateTarget();

            return;
        }
        _target = _handler.GetTarget().transform;

        if((_target.position -  _body.position).sqrMagnitude < 0.1) {
            _handler.MoveToNext();
        } else
        {
            //init
            RaycastHit hit;
            Vector3 fwd = _body.forward;

            if (Physics.SphereCast(_body.position, _raycastSphereRadius, fwd, out hit, _raycastDistance))
            {
                if (hit.transform.tag != "Foe" && hit.transform.tag != "Enviroment" && hit.transform.gameObject.GetComponent<IDestroyable>() != null)
                {
                    _handler.SetTarget(hit.transform.gameObject);
                    _handler.MoveToNext();
                    return;
                } else
                {
                    bool isSet = false;
                    Vector3 newDirection = new Vector3(0, 0, 0);
                    Vector3 bufVector = new Vector3();
                    for (int i = -1; i <= 1; i++)
                    {
                        for(int j = -1; j <= 1; j++)
                        {
                            if (i == j && i == 0) continue;
                            bufVector.Set(i, 0, j);
                            if (!Physics.SphereCast(_body.position, _raycastSphereRadius, bufVector, out hit, _raycastDistance) && (!isSet || (bufVector - _body.forward).sqrMagnitude < (newDirection - _body.forward).sqrMagnitude))
                            {
                                isSet = true;
                                newDirection.Set(i, 0, j);
                            } 
                        }
                    }

                    _body.rotation = Quaternion.LookRotation(newDirection);

                    _body.Translate(newDirection * _speed * (float)delta);
                }
            } else
            {
                Vector3 look = _target.position - _body.position;
                look.y = _body.position.y;
                
                _body.rotation = Quaternion.LookRotation(look);
                _body.Translate(look.normalized * _speed * (float)delta);

                _handler.UpdateTarget();
            }
        }
    }

}
