using KJW.Code.Input;
using UnityEngine;

public class Boat : MonoBehaviour,IInteractable
{
    [field: SerializeField] public BoatSOScript BoatSO { get; private set; }
    [SerializeField] private InputReader inputReader;
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
            _boatMovement.SetMove(inputReader.Dir);
        }
        else
        {
            _boatMovement.SetMove(Vector2.zero);
        }
    }
    public void Enter(GameObject target)
    {
        _isPlayerInBoat = true;
        _target.transform.position = gameObject.transform.position;
        _target.transform.parent = gameObject.transform;
        _target.SetActive(false);
        _target = target;
    }
    public void Exit(GameObject target)
    { 
        _target.SetActive(true);
        _target.transform.position = gameObject.transform.position + gameObject.transform.up;
        _target.transform.parent = null;
        _target = null;
        _isPlayerInBoat = false;
    }

    public void Interact(GameObject user)
    {
        if(!_isPlayerInBoat)
        {
            Enter(user);
        }
        else
        {
            Exit(user);
        }
    }
}
