using System;
using Member.JJW.Code.Interface;
using Member.JJW.EventChannels;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TestPlayer : MonoBehaviour
{
    [SerializeField] private TransformEventChannel transformEventChannel;
    [SerializeField] private float itemDetectionRadius;
    [SerializeField] private LayerMask mask;
    [SerializeField] private int damage =10;
    private bool _isCanInteract;
    private IInteractable _interactable;
    private Vector2 _moveDir;
    private Rigidbody2D _rb;
    private bool _isClicked= false;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            _isClicked = true;
            if (_isCanInteract)
            {
                _interactable.Interaction(damage);
            }
        }
        else
        {
            _isClicked = false;
        }
        
    }
    private void OnMove(InputValue value)
    {
        _moveDir = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = _moveDir * 5;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, itemDetectionRadius);
    }
}
