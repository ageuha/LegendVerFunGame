using System;
using Code.Core.GlobalSO;
using Code.Core.Pool;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YTH.Code.Item;

namespace Code.UI.ItemAddedUI {
    [RequireComponent(typeof(CanvasGroup))]
    public class AddedItemUI : MonoBehaviour, IPoolable {
        [SerializeField] private TextMeshProUGUI header;
        [SerializeField] private TextMeshProUGUI amount;
        [SerializeField] private Image icon;
        [SerializeField] private CanvasGroup group;
        [SerializeField] private float displayDuration = 2f;
        [SerializeField] private float fadeDuration = .5f;
        [SerializeField] private TweeningInfoSO scaleTweeningInfo;

        private bool _isFading;
        private float _endTime;
        private Tweener _fadeTweener;
        private Tweener _scaleTweener;
        private TypeSafePoolFactory<AddedItemUI> _factory;

        private void Reset() {
            group ??= GetComponent<CanvasGroup>();
        }

        private void Awake() {
            if (!header || !amount) {
                throw new Exception(
                    "AddedItemUI: One or more TextMeshProUGUI references are not set in the inspector.");
            }

            _fadeTweener = group.DOFade(0, fadeDuration)
                .OnComplete(() => _factory.Push(this)).SetAutoKill(false).Pause();
            _scaleTweener = transform.DOScale(scaleTweeningInfo.Position, scaleTweeningInfo.Duration)
                .SetEase(scaleTweeningInfo.EasingType)
                .SetLoops(2, LoopType.Yoyo)
                .SetAutoKill(false)
                .Pause();
        }

        public void SetItemAddedInfo(ItemData itemData, TypeSafePoolFactory<AddedItemUI> factory) {
            icon.sprite = GetItemData.Instance.ItemDataListSO[itemData.ItemID].Icon;
            header.text = GetItemData.Instance.ItemDataListSO[itemData.ItemID].ItemName;
            amount.text = $"x{itemData.Count}";
            _scaleTweener.Restart();
            _factory = factory;
        }

        public int InitialCapacity => 10;

        private void Update() {
            if (!_isFading && Time.time >= _endTime) {
                _isFading = true;
                _fadeTweener.Restart();
            }
        }

        public void OnPopFromPool() {
            group.alpha = 1;
            _endTime = Time.time + displayDuration;
            _isFading = false;
        }

        public void OnReturnToPool() {
        }
    }
}