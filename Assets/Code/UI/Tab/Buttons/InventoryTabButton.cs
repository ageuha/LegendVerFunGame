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
            _originAnchorPos = visual.rectTransform.anchoredPosition;
        }

        protected override void OnActiveTab() {
            element.Value.EnableFor(Empty.New);
            AnimateReversePositionTween();
        }

        protected override void OnDeactiveTab() {
            element.Value.Disable();
            AnimatePositionToOrigin();
        }

        public override void OnPointerEnter(PointerEventData eventData) {
            base.OnPointerEnter(eventData);
            if (IsSelected) return;
            AnimatePositionTween();
        }

        public override void OnPointerExit(PointerEventData eventData) {
            base.OnPointerExit(eventData);
            if (IsSelected) return;
            AnimatePositionToOrigin();
        }

        public override void OnSelect(BaseEventData eventData) {
            base.OnSelect(eventData);
            if (IsSelected) return;
            AnimatePositionTween();
        }

        public override void OnDeselect(BaseEventData eventData) {
            base.OnDeselect(eventData);
            if (IsSelected) return;
            AnimatePositionToOrigin();
        }

        private void AnimatePositionTween() {
            _positionTween?.Kill();
            _positionTween =
                visual.rectTransform.DOAnchorPos(_originAnchorPos + (Vector2)tweeningInfo.Position,
                        tweeningInfo.Duration)
                    .SetEase(tweeningInfo.EasingType);
        }

        private void AnimateReversePositionTween() {
            _positionTween?.Kill();
            _positionTween =
                visual.rectTransform.DOAnchorPos(_originAnchorPos - (Vector2)tweeningInfo.Position,
                        tweeningInfo.Duration)
                    .SetEase(tweeningInfo.EasingType);
        }

        private void AnimatePositionToOrigin() {
            _positionTween?.Kill();
            _positionTween =
                visual.rectTransform.DOAnchorPos(_originAnchorPos, tweeningInfo.Duration)
                    .SetEase(tweeningInfo.EasingType);
        }
    }
}