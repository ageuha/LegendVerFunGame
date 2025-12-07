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
        private Tweener _positionTween;

        protected override void AfterTabClicked() {
        }

        protected override void AfterInit() {
            _originAnchorPos = visual.rectTransform.anchoredPosition;

            _positionTween =
                visual.rectTransform.DOAnchorPosX(_originAnchorPos.x, tweeningInfo.Duration).SetAutoKill(false)
                    .SetEase(tweeningInfo.EasingType).Pause();
        }

        protected override void OnDestroy() {
            base.OnDestroy();
            _positionTween?.Kill();
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
            _positionTween.Pause();
            _positionTween.ChangeValues(visual.rectTransform.anchoredPosition,
                _originAnchorPos + (Vector2)tweeningInfo.Position).Restart();
        }

        private void AnimateReversePositionTween() {
            _positionTween.Pause();
            _positionTween.ChangeValues(visual.rectTransform.anchoredPosition,
                _originAnchorPos - (Vector2)tweeningInfo.Position).Restart();
        }

        private void AnimatePositionToOrigin() {
            _positionTween.Pause();
            _positionTween.ChangeValues(visual.rectTransform.anchoredPosition,
                _originAnchorPos).Restart();
        }
    }
}