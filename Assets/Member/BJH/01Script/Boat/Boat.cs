using Code.Core.Utility;
using Member.KJW.Code.Player;
using UnityEngine;

public class Boat : MonoBehaviour, IInteractable
{
    [field: SerializeField] public BoatSOScript BoatSO { get; private set; }
    private BoatMovement _boatMovement;
    private bool _isPlayerInBoat;
    private GameObject _target;
    private Vector3 mouseWorldPos;
    void Awake()
    {
        _boatMovement = GetComponent<BoatMovement>();
    }
    void FixedUpdate()
    {
        if (_isPlayerInBoat)
        {
            Vector2 mousePos = _target.GetComponent<Player>().InputReader.MousePos;
            Vector3 screenPos = new Vector3(mousePos.x,mousePos.y,-Camera.main.transform.position.z);
            mouseWorldPos = Camera.main.ScreenToWorldPoint(screenPos);

            _boatMovement.SetMove((mouseWorldPos-transform.position).normalized);
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
        if (!_isPlayerInBoat)
        {
            Enter(user);
        }
        else
        {
            Exit();
        }
    }
}
