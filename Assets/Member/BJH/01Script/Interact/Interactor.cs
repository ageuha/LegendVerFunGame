using Code.Core.Utility;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius;
    [SerializeField] private LayerMask _interactionMask;
    private Collider2D[] _colliders;
    private IInteractable _interactableThing;

    void FixedUpdate()
    {
        CheckInteract();
    }

    private void CheckInteract()
    {
        _colliders = Physics2D.OverlapCircleAll(_interactionPoint.position, _interactionPointRadius, _interactionMask);
        
        foreach (Collider2D c in _colliders)
        {
            if(c.TryGetComponent<IInteractable>(out _interactableThing))
            {
                return;
            }
        }
    }
    public void Interact()
    {
        _interactableThing?.Interact(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}
