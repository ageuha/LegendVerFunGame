using UnityEngine;

public interface IRideable
{
    public void Enter(GameObject target);
    public void Exit(GameObject target);
    public void SetNear(bool isNear, GameObject target);
    public bool CanEnter(GameObject target);
}
