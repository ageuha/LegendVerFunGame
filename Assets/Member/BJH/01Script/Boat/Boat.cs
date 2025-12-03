using Code.Core.Utility;
using KJW.Code.Player;
using UnityEngine;

public class Boat : MonoBehaviour,IInteractable
{
    [field: SerializeField] public BoatSOScript BoatSO { get; private set; }
    private BoatMovement _boatMovement;
    private bool _isPlayerInBoat;
    private GameObject _target;

    void Awake()
    {
        _boatMovement = GetComponent<BoatMovement>();

    }
    void FixedUpdate()
    {
        if (_isPlayerInBoat)
        {
            _boatMovement.SetMove(_target.GetComponent<Player>().InputReader.Dir);
            _target.transform.localPosition = Vector2.zero;
        }
        else
        {
            _boatMovement.SetMove(Vector2.zero);
        }
    }
    public void Enter(GameObject target)
    {
        _target = target;
        _isPlayerInBoat = true;
        _target.transform.parent = transform;
        _target.GetComponent<AgentMovement>().StopMove();
        _target.transform.localPosition = Vector2.zero;
    }
    public void Exit()
    { 
        _target.GetComponent<AgentMovement>().SetMove(Vector2.zero);
        _target.transform.position = transform.position + transform.up;
        _target.transform.parent = null;
        _isPlayerInBoat = false;
        _target = null;
    }

    public void Interact(GameObject user)
    {
        if(!_isPlayerInBoat)
        {
            Enter(user);
        }
        else
        {
            Exit();
        }
    }
}
