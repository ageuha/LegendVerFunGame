using UnityEngine;

namespace Code.AnimatorSystem {
    [RequireComponent(typeof(Animator))]
    public class AnimatorCompo : MonoBehaviour {
        [SerializeField] protected Animator animator;

        protected virtual void Reset() {
            animator ??= GetComponent<Animator>();
        }

        public virtual void SetValue(int hash, float value) => animator.SetFloat(hash, value);
        public virtual void SetValue(int hash, bool value) => animator.SetBool(hash, value);
        public virtual void SetValue(int hash, int value) => animator.SetInteger(hash, value);
        public virtual void SetValue(int hash) => animator.SetTrigger(hash);

        public float AnimationSpeed {
            get => animator.speed;
            set => animator.speed = value;
        }
    }
}