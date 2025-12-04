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
                Vector2 mousePos = _target.GetComponent<Player>().InputReader.MousePos;
                Vector3 screenPos = new Vector3(mousePos.x, mousePos.y, -Camera.main.transform.position.z);
                mouseWorldPos = Camera.main.ScreenToWorldPoint(screenPos);

                _boatMovement.SetMove(mouseWorldPos - transform.position, _isPlayerMousePressed);
                _target.transform.localPosition = Vector2.zero;
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