using Member.JJW.Code.Interface;
using Member.KJW.Code.Input;
using Member.KJW.Code.Player;
using UnityEngine;

namespace Member.BJH._01Script.Boat
{
    public class Boat : MonoBehaviour, IInteractable<GameObject>
    {
        [field: SerializeField] public BoatSOScript BoatSO { get; private set; }
        [SerializeField] private InputReader inputreader;
        private BoatMovement _boatMovement;
        private bool _isPlayerInBoat;
        private bool _isPlayerMousePressed;
        private GameObject _target;
        private Vector3 mouseWorldPos;
        private void Awake()
        {
            _boatMovement = GetComponent<BoatMovement>();
        }
        private void OnEnable()
        {
            inputreader.OnAttacked += PlayerPressMouse;
            inputreader.OnAttackReleased += NoPlayerPressMouse;
        }
        private void OnDisable()
        {
            inputreader.OnAttacked -= PlayerPressMouse;
            inputreader.OnAttackReleased -= NoPlayerPressMouse;
        }

        private void PlayerPressMouse()
        {
            if (_isPlayerInBoat)
                _isPlayerMousePressed = true;
            else
                _isPlayerMousePressed = false;
        }
        private void NoPlayerPressMouse()
        {
            if (_isPlayerInBoat)
                _isPlayerMousePressed = false;
        }
        private void FixedUpdate()
        {
            if (_isPlayerInBoat)
            {
                mouseWorldPos = Camera.main.ScreenToWorldPoint(_target.GetComponent<Player>().InputReader.MousePos);

                _boatMovement.SetMove(mouseWorldPos - transform.position, _isPlayerMousePressed);
            }
            else
            {
                _boatMovement.SetMove(Vector2.zero, false);
                _boatMovement.StopMove();
            }
        }
        public void Enter(GameObject target)
        {
            _target = target;
            _isPlayerInBoat = true;
            _target.GetComponent<AgentMovement>().StopMove();
            _target.transform.SetParent(transform);
            _target.transform.localPosition = Vector2.zero;
        }
        public void Exit()
        {
            _target.GetComponent<AgentMovement>().RestartMove();
            _target.transform.position = transform.position + transform.up;
            _target.transform.SetParent(null);
            _isPlayerInBoat = false;
            _target = null;
            _isPlayerMousePressed = false;
            _boatMovement.StopMove();
        }


        public void Interaction(GameObject user)
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
}