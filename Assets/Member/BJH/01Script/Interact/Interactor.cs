using Code.Core.Utility;
using KJW.Code.Input;
using KJW.Code.Player;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius;
    [SerializeField] private LayerMask _interactionMask;
    private IInteractable _interactableThing;
    private void CheckInteract()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_interactionPoint.position, _interactionPointRadius, _interactionMask);
         
        foreach (Collider2D c in colliders)
        {
            if(c.TryGetComponent<IInteractable>(out _interactableThing))
            {
                return;
            }
        }
    }
    public void Interact()
    {
        CheckInteract();
        _interactableThing?.Interact(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}
