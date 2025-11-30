using Code.Core.Utility;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.UI.Tab {
    [RequireComponent(typeof(Button))]
    public abstract class AbstractTabButton<T> : MonoBehaviour, ITabButton, IPointerEnterHandler, IPointerExitHandler {
        [SerializeField] protected TabGroup tabGroup;
        [SerializeField] protected SerializeHelper<IUIElement<T>> element;

        protected Button Button;
        protected bool IsActive;
        protected bool IsInactive => !IsActive;

        public int Index => transform.GetSiblingIndex();

        protected virtual void Reset() {
            if (transform.parent.TryGetComponent<TabGroup>(out var group)) {
                tabGroup = group;
            }
        }

        private void Awake() {
            tabGroup.ResisterTab(this);
            Button = GetComponent<Button>();
            Button.onClick.AddListener(OnTabClicked);
            AfterInit();
        }

        private void OnTabClicked() {
            tabGroup.OnTabSelected(this);
            AfterTabClicked();
        }

        protected abstract void AfterTabClicked();

        protected abstract void AfterInit();

        protected abstract void OnActiveTab();

        protected abstract void OnDeactiveTab();

        public void ActivateTab() {
            if (IsActive) return;
            IsActive = true;
            OnActiveTab();
        }

        public void DeactivateTab() {
            if (!IsActive) return;
            IsActive = false;
            OnDeactiveTab();
        }

        public virtual void OnPointerEnter(PointerEventData eventData) {
        }

        public virtual void OnPointerExit(PointerEventData eventData) {
        }
    }
}