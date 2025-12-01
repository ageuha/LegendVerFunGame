using Code.Core.Utility;
using Input;
using UnityEngine;

public class Boat : MonoBehaviour, IRideable
{
    public BoatSOScript BoatSO{get; private set;}
    [SerializeField] private InputReader inputReader;
    private BoatMovement _boatMovement;
    private bool _isPlayerNearTheBoat;
    private bool _isPlayerInBoat;
    private GameObject _target;

    void Awake()
    {
        _boatMovement = GetComponent<BoatMovement>();
    }
    void OnEnable()
    {
        inputReader.OnAttacked += TryEnter;
        inputReader.OnRolled += TryExit;
    }
    void OnDisable()
    {
        inputReader.OnAttacked -= TryEnter;
        inputReader.OnRolled -= TryExit;
    }

    private void TryEnter()
    {
        if (CanEnter(_target))
        {
            Enter(_target);
        }
    }

    private void TryExit()
    {
        if (_target != null)
        {
            Exit(_target);
        }
    }
    public void Enter(GameObject target)
    {
        if(target == null && !_isPlayerInBoat)
        {
        _isPlayerInBoat = true;
        _target = target;
        _target.transform.position = gameObject.transform.position;
        _target.transform.parent = gameObject.transform;
        }
    }
    public void Exit(GameObject target)
    {
        if(target != null && _isPlayerInBoat)
        {
        _target.transform.position = gameObject.transform.position + gameObject.transform.up;
        _target.transform.parent = null;
        _target = null;
        _isPlayerInBoat = false;
        }
    }

    public bool CanEnter(GameObject target)
    {
        return _isPlayerNearTheBoat && !_isPlayerInBoat;//플레이어가 보트 근처에 있고
    }
    public void SetNear(bool isNear, GameObject target)
    {
        _isPlayerNearTheBoat = isNear;//isNear의 값에 따라 _isPlayerNearTheBoat를 수정
        _target = target;
    }
}
