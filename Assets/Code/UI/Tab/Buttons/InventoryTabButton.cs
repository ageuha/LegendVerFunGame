using Code.Core.GlobalSO;
using Code.Core.GlobalStructs;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.UI.Tab.Buttons {
    public class InventoryTabButton : AbstractTabButton<Empty> {
        [SerializeField] private Image visual;
        [SerializeField] private TweeningInfoSO tweeningInfo;

        private Vector2 _originAnchorPos;
        private Tween _positionTween;

        protected override void AfterTabClicked() {
        }

        protected override void AfterInit() {
        }

        protected override void OnActiveTab() {
            element.Value.EnableFor(Empty.New);
        }

        protected override void OnDeactiveTab() {
            element.Value.Disable();
        }

        public override void OnPointerEnter(PointerEventData eventData) {
            base.OnPointerEnter(eventData);
            if (IsActive) return;
            _originAnchorPos = visual.rectTransform.anchoredPosition;
            _positionTween?.Kill();
            _positionTween =
                visual.rectTransform.DOAnchorPos(_originAnchorPos + tweeningInfo.Position,
                        tweeningInfo.Duration)
                    .SetEase(tweeningInfo.EasingType);
        }

        public override void OnPointerExit(PointerEventData eventData) {
            base.OnPointerExit(eventData);
            if (IsInactive) return;
            _positionTween?.Kill();
            _positionTween =
                visual.rectTransform.DOAnchorPos(_originAnchorPos, tweeningInfo.Duration)
                    .SetEase(tweeningInfo.EasingType);
        }
    }
}