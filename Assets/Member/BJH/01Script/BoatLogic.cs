using Code.Core.Utility;
using KJW.Code.Player;
using UnityEngine;

public class BoatLogic : MonoBehaviour
{
    [SerializeField] private BoatSOsceipt boatSO;
    private PlayerInput playerInput;
    private bool _isCoolTime;
    private bool _isAuto;
    private float _rowPower;
    private Rigidbody2D _rigid;

    private bool _isRowing = true;

    private Vector2 _mousePos;
    void Awake()
    {
        _isAuto = boatSO.IsAuto;
        _rowPower = boatSO.RowPower;
        _rigid = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }
    void OnEnable()
    {
        playerInput.OnAttacked += Rowing;
    }
    void ChangeMousePosTOWorld(Vector2 mousePosition)
    {
        Camera cam = Camera.main;

        
    }
    public void Rowing()
    {
        if (_isRowing)
        {
        _mousePos = playerInput.MousePos;
        Vector2 boatPos = transform.position;
        Vector2 dir = (_mousePos - boatPos).normalized;
            
            Logging.Log("노젓기!!");
            _rigid.AddForce(dir * _rowPower, ForceMode2D.Impulse);
        }
    }
    void OnDisable()
    {
        playerInput.OnAttacked -= Rowing;
    }
}
