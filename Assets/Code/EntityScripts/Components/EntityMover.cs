using System;
using Code.EntityScripts.BaseClass;
using Code.EntityScripts.Interface;
using UnityEngine;

namespace Code.EntityScripts.Components {
    public class EntityMover : MonoBehaviour, IEntityModule {
        [SerializeField] private Rigidbody2D rb;
        private float _moveSpeed;

        private Vector2 _movementInput;

        private void Reset() {
            rb ??= transform.root.GetComponentInChildren<Rigidbody2D>(true);
        }

        public void StopImmediately() {
            _movementInput = Vector2.zero;
            rb.linearVelocity = Vector2.zero;
        }

        public void SetMovementInput(Vector2 input) {
            _movementInput = input.normalized;
        }

        public void SetSpeed(float speed) {
            _moveSpeed = speed;
        }

        private void FixedUpdate() {
            rb.linearVelocity = _movementInput * _moveSpeed;
        }

        public void Initialize(Entity owner) {
            rb ??= owner.GetComponent<Rigidbody2D>();
            _moveSpeed = owner.Data.MoveSpeed;
        }
    }
}