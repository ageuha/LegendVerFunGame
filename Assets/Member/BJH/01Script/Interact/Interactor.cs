using Member.JJW.Code.Interface;
using UnityEngine;

namespace Member.BJH._01Script.Interact
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private Transform _interactionPoint;
        [SerializeField] private float _interactionPointRadius;
        [SerializeField] private LayerMask _interactionMask;
        private IInteractable<GameObject> _interactableThing;
        private void CheckInteract()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_interactionPoint.position, _interactionPointRadius, _interactionMask);

            foreach (Collider2D c in colliders)
            {
                if (c.TryGetComponent(out _interactableThing))
                {
                    return;
                }
            }
        }
        public void Interact()
        {
            CheckInteract();
            _interactableThing?.Interaction(gameObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
        }
    }
}