using Code.Core.Utility;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius;
    [SerializeField] private LayerMask _interactionMask;
    private Collider2D[] _colliders = new Collider2D[3];
    private IInteractable _interactableThing;

    void FixedUpdate()
    {
        _colliders = Physics2D.OverlapCircleAll(_interactionPoint.position, _interactionPointRadius,_interactionMask);
        foreach (Collider2D item in _colliders)
        {
            if(item.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                _interactableThing = interactable;
                Logging.Log(_interactableThing);
            }
            else
            {
                _interactableThing = null;
            }
        }
    }

    private void Interacting()
    {
        if(_interactableThing != null)
        {
            _interactableThing.Interact(gameObject);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_interactionPoint.position,_interactionPointRadius);
    }
}
