using Code.Core.Utility;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.UI.Tab {
    public abstract class AbstractTabButton<T> : Selectable, ITabButton, IPointerClickHandler, ISubmitHandler {
        [SerializeField] protected TabGroup tabGroup;
        [SerializeField] protected SerializeHelper<IUIElement<T>> element;

        protected bool IsSelected;

        public int Index => transform.GetSiblingIndex();

#if UNITY_EDITOR
        protected override void Reset() {
            if (transform.parent.TryGetComponent<TabGroup>(out var group)) {
                tabGroup = group;
            }
        }
        
#endif

        protected override void Awake() {
            tabGroup.ResisterTab(this);
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
            if (IsSelected) return;
            IsSelected = true;
            OnActiveTab();
        }

        public void DeactivateTab() {
            if (!IsSelected) return;
            IsSelected = false;
            OnDeactiveTab();
        }

        public void OnPointerClick(PointerEventData eventData) {
            OnTabClicked();
        }

        public void OnSubmit(BaseEventData eventData) {
            OnTabClicked();
        }
    }
}