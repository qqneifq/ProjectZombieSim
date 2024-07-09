using UnityEngine;

public interface IZombieChainHandler 
{
    public void SetTarget(GameObject target);
    public GameObject GetTarget();
    public void MoveToNext();
    public void UpdateTarget();

    public void SetAnimationAttackSpeed(float speed);

    public void SetAnimtionWalkSpeed(float speed);
}
