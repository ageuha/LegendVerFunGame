using Code.Core.Pool;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using YTH.Code.Inventory;
using YTH.Code.Item;

namespace Code.UI.ItemAddedUI {
    [RequireComponent(typeof(CanvasGroup))]
    public class ItemAddedCanvas : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        [SerializeField] private InventoryAddEventChannel inventoryAddEventChannel;
        [SerializeField] private AddedItemUI prefab;
        [SerializeField] private CanvasGroup group;
        [SerializeField] private Transform contentsParent;
        
        private TypeSafePoolFactory<AddedItemUI> _factory;
        private Tweener _fadeTweener;

        private void Reset() {
            group ??= GetComponent<CanvasGroup>();
        }

        private void Awake() {
            _factory = new TypeSafePoolFactory<AddedItemUI>(prefab, prefab.InitialCapacity, contentsParent);
            _fadeTweener = group.DOFade(0.5f, .2f).SetAutoKill(false).Pause();
            inventoryAddEventChannel.OnEvent += HandleItemAdded;
        }

        private void OnDestroy() {
            inventoryAddEventChannel.OnEvent -= HandleItemAdded;
        }

        private void HandleItemAdded(ItemData obj) {
            var ui = _factory.Pop(contentsParent);
            ui.SetItemAddedInfo(obj, _factory);
        }

        public void OnPointerEnter(PointerEventData eventData) {
            _fadeTweener.PlayForward();
        }

        public void OnPointerExit(PointerEventData eventData) {
            _fadeTweener.PlayBackwards();
        }
    }
}